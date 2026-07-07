using System;
using Cysharp.Threading.Tasks;

namespace Core.Tools
{
    public static class AsyncTools
    {
        public static async UniTask DelayActionAsync(float delaySeconds, Action action)
        {
            await UniTask.WaitForSeconds(delaySeconds);

            action?.Invoke();
        }
    }
}