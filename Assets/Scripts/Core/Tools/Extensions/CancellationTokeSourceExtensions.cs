using System.Threading;

namespace Core.Tools.Extensions
{
    public static class CancellationTokeSourceExtensions
    {
        public static void CancelAndDispose(this CancellationTokenSource tokenSource)
        {
            if (tokenSource is not null && tokenSource.IsCancellationRequested is false)
            {
                tokenSource.Cancel();
                tokenSource.Dispose();
            }
        }
    }
}