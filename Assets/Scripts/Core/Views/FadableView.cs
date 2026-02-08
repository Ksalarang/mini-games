using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core.Views
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadableView : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        private void OnEnable()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
        }

        public UniTask FadeIn(float duration = 0.25f)
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 0f;
            
            return canvasGroup.DOFade(1f, duration).ToUniTask();
        }

        public UniTask FadeOut(float duration = 0.25f)
        {
            canvasGroup.alpha = 1f;
            
            return canvasGroup.DOFade(0f, duration)
                .OnComplete(() => canvasGroup.gameObject.SetActive(false))
                .ToUniTask();
        }
    }
}