using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : UIPanel
{
    private void Awake()
    {
        _gameStateController.OnBeforeGameStateEvent += EnablePanel;
        _gameStateController.OnGameStateEvent += DisablePanel;
    }

    public void OnPlayButton()
    {
        _gameStateController.SetGameState();
    }
}
