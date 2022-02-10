using UnityEngine;

public class ResultPanelAnimationController : UIPanelAnimationController
{
    [SerializeField] private Transform _winPanelContent;
    [SerializeField] private Transform _losePanelContent;

    [SerializeField] private float _animationsDuration;

    public void StartWinPanelContentAppereanceAnimation() => StartAppearanceAnimation(_winPanelContent, _animationsDuration);
    public void StartLosePanelContentAppereanceAnimation() => StartAppearanceAnimation(_losePanelContent, _animationsDuration);
}
