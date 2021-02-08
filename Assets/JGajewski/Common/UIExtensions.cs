using Cysharp.Threading.Tasks;
using UnityEngine;

namespace JGajewski.Common
{
    public static class UIExtensions
    {
        public static async UniTask FadeGroup(this CanvasGroup targetGroup, 
            float fromValue, float toValue, float duration)
        {
            targetGroup.alpha = fromValue;

            for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
            {
                targetGroup.alpha = Mathf.SmoothStep(fromValue, toValue, t);
                await UniTask.Yield();
            }
            targetGroup.alpha = toValue;
        }
    }
}
