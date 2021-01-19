using System;
using System.Threading;

namespace Orbital.Core
{
    public interface IStateMachine<out TState, in TCommand> : IService
        where TState : Enum
        where TCommand : Enum
    {
        TState CurrentState { get; }

        void AutoTransitNext(TCommand command, CancellationToken cancellationToken, Action synchronizedAction = null);

        bool TryTransitNext(TCommand command, Action synchronizedAction = null);
    }
}