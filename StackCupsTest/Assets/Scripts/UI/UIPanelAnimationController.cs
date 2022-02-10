using UnityEngine;
using DG.Tweening;

public abstract class UIPanelAnimationController : MonoBehaviour
{
    protected void StartAppearanceAnimation(Transform animatedTransform, float duration)
    {
        animatedTransform.localScale = Vector3.zero;
        animatedTransform.DOScale(Vector3.one, duration).SetEase(Ease.OutCubic);
    }

    protected void StartDisappearAnimation(Transform animatedTransform, float duration)
    {
        animatedTransform.DOScale(Vector3.zero, duration).SetEase(Ease.OutCubic)
            .OnComplete(() => { animatedTransform.gameObject.SetActive(false); });
    }
}
