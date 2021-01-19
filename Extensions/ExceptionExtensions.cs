﻿using System;
using System.Linq;
using System.Threading;

namespace Orbital.Core
{
    public static class ExceptionExtensions
    {
        public static bool IsAcknowledgingOperationCancelledException(this Exception exception, CancellationToken cancellationToken)
        {
            switch (exception)
            {
                case OperationCanceledException operationCanceledException:
                {
                    var cancellationRequested = operationCanceledException.CancellationToken.IsCancellationRequested;
                    var cancellationTokenIsTheSame = operationCanceledException.CancellationToken.Equals(cancellationToken);
                    if (cancellationRequested && cancellationTokenIsTheSame) return true;
                    break;
                }
                case AggregateException aggregateException:
                {
                    var allInnerExceptionsAreAcknowledgingOperationCancelledException = aggregateException.Flatten().InnerExceptions.All(
                        innerException =>
                        {
                            if (!(innerException is
                                    OperationCanceledException
                                    operationCanceledException
                                )) return false;
                            var cancellationRequested
                                = operationCanceledException
                                    .CancellationToken
                                    .IsCancellationRequested;
                            var
                                cancellationTokenIsTheSame
                                    = operationCanceledException
                                        .CancellationToken
                                        .Equals(
                                            cancellationToken);
                            return
                                cancellationRequested &&
                                cancellationTokenIsTheSame;
                        }
                    );
                    if (allInnerExceptionsAreAcknowledgingOperationCancelledException) return true;
                    break;
                }
            }
            return false;
        }
    }
}