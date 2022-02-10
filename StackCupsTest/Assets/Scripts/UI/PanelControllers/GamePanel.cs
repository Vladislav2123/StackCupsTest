using Architecture.LevelManager;

public class GamePanel : UIPanel
{
    private void Awake()
    {
        _gameStateController.OnBeforeGameStateEvent += DisablePanel;
        _gameStateController.OnGameStateEvent += EnablePanel;
        _gameStateController.OnAfterGameStateEvent += DisablePanel;
    }

    public void OnRestartButton() => LevelManager.ReloadLevel();
}
