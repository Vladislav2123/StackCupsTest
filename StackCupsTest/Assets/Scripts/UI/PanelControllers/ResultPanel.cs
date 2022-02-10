using Architecture.LevelManager;
using UnityEngine;

[RequireComponent(typeof(ResultPanelAnimationController))]
public class ResultPanel : UIPanel
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

    private ResultPanelAnimationController _animationController;

    private void Awake()
    {
        _animationController = GetComponent<ResultPanelAnimationController>();

        _gameStateController.OnBeforeGameStateEvent += DisablePanel;
        _gameStateController.OnAfterGameStateEvent += EnablePanel;
        _gameStateController.OnAfterGameStateEvent += DefineTitle;
    }

    private void DefineTitle()
    {
        if (_gameStateController.IsFinished) EnableWinPanel();
        else EnableLosePanel();
    }

    private void EnableWinPanel()
    {
        _winPanel.SetActive(true);
        _animationController.StartWinPanelContentAppereanceAnimation();
    }

    private void EnableLosePanel()
    {
        _losePanel.SetActive(true);
        _animationController.StartLosePanelContentAppereanceAnimation();
    }

    public void OnNextButton() => LevelManager.LoadNextLevel();
    public void OnRetryButton() => LevelManager.ReloadLevel();
}
