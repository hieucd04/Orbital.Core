using System;
using System.Diagnostics;

namespace Orbital.Core
{
    public static class ProcessExtensions
    {
        public static bool IsRunning(this Process process)
        {
            if (process == null)
                throw new ArgumentNullException(nameof(process));

            try
            {
                var processId = process.Id;
                return !process.HasExited && IsRunning(processId, out _);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        static bool IsRunning(int processId, out Process process)
        {
            process = null;

            try
            {
                process = Process.GetProcessById(processId);
            }
            catch (Exception exception)
            {
                if (exception is ArgumentException || exception is InvalidOperationException)
                    return false;

                throw;
            }

            return true;
        }
    }
}