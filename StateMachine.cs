using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace Orbital.Core
{
    internal class StateMachine<TState, TCommand> : IStateMachine<TState, TCommand>, IDisposable
        where TState : Enum
        where TCommand : Enum
    {
        readonly CountdownEvent _activeStateTransitionCountdownEvent;
        readonly object _disposalLock;
        readonly Dictionary<Transition, TState> _possibleTransitions;
        readonly ManualResetEventSlim _stateChangeManualResetEventSlim;
        readonly object _stateTransitionLock;
        TState _currentState;
        bool _waitingForDisposal;

        public TState CurrentState
        {
            get => _currentState;
            private set
            {
                _currentState = value;
                _stateChangeManualResetEventSlim.Set();
            }
        }

        public StateMachine(Dictionary<Transition, TState> possibleTransitions, TState initialState)
        {
            _disposalLock = new object();
            _waitingForDisposal = false;
            _stateTransitionLock = new object();
            _possibleTransitions = possibleTransitions;
            _activeStateTransitionCountdownEvent = new CountdownEvent(1);
            _stateChangeManualResetEventSlim = new ManualResetEventSlim(false);

            CurrentState = initialState;
        }

        public void AutoTransitNext(TCommand command, CancellationToken cancellationToken, Action synchronizedAction)
        {
            _activeStateTransitionCountdownEvent.AddCount();
            try
            {
                if (_waitingForDisposal) throw new ObjectDisposedException(nameof(StateMachine<TState, TCommand>));

                TState nextState;
                while (true)
                {
                    Monitor.Enter(_stateTransitionLock);
                    if (TryGetNext(command, out nextState)) break;
                    Monitor.Exit(_stateTransitionLock);

                    _stateChangeManualResetEventSlim.Reset();
                    _stateChangeManualResetEventSlim.Wait(cancellationToken);
                    if (cancellationToken.IsCancellationRequested) return;
                }

                try
                {
                    if (EqualityComparer<TState>.Default.Equals(nextState, CurrentState))
                    {
                        var exceptionMessage = $"{nameof(AutoTransitNext)} cannot be used to perform self-transition. " +
                                               $"Current state: {CurrentState}.";
                        throw new InvalidConstraintException(exceptionMessage);
                    }

                    CurrentState = nextState;
                    synchronizedAction?.Invoke();
                }
                finally { Monitor.Exit(_stateTransitionLock); }
            }
            finally { _activeStateTransitionCountdownEvent.Signal(); }
        }

        public void Dispose()
        {
            lock (_disposalLock)
            {
                if (_waitingForDisposal) return;
                _waitingForDisposal = true;
            }

            _stateChangeManualResetEventSlim.Set();
            _activeStateTransitionCountdownEvent.Signal();
            _activeStateTransitionCountdownEvent.Wait();

            _stateChangeManualResetEventSlim.Dispose();
            _activeStateTransitionCountdownEvent.Dispose();
        }

        public bool TryTransitNext(TCommand command, Action synchronizedAction)
        {
            _activeStateTransitionCountdownEvent.AddCount();
            try
            {
                if (_waitingForDisposal) throw new ObjectDisposedException(nameof(StateMachine<TState, TCommand>));
                lock (_stateTransitionLock)
                {
                    if (!TryGetNext(command, out var nextState)) return false;
                    CurrentState = nextState;
                    synchronizedAction?.Invoke();
                    return true;
                }
            }
            finally { _activeStateTransitionCountdownEvent.Signal(); }
        }

        bool TryGetNext(TCommand command, out TState nextState)
        {
            var transition = new Transition(CurrentState, command);
            return _possibleTransitions.TryGetValue(transition, out nextState);
        }

        public class Transition
        {
            readonly TCommand _command;
            readonly TState _currentState;

            public Transition(TState currentState, TCommand command)
            {
                _currentState = currentState;
                _command = command;
            }

            public override bool Equals(object obj)
            {
                return obj is Transition other && _currentState.Equals(other._currentState) && _command.Equals(other._command);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (EqualityComparer<TCommand>.Default.GetHashCode(_command) * 397) ^
                           EqualityComparer<TState>.Default.GetHashCode(_currentState);
                }
            }
        }
    }
}