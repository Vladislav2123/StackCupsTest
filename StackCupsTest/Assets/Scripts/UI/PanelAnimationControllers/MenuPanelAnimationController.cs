using DG.Tweening;
using UnityEngine;

public class MenuPanelAnimationController : UIPanelAnimationController
{
    [SerializeField] private Transform _playButtonText;
    [SerializeField] private float _playButtnonScaleChangeFactor;
    [SerializeField] private float _playButtonAnimationDuration;

    private void OnEnable() => _playButtonText.DOScale(_playButtnonScaleChangeFactor, _playButtonAnimationDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InCubic);
}
