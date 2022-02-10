using UnityEngine;
using Zenject;

public class GameStateControllerInstaller : MonoInstaller
{
    [SerializeField] private GameStateController _gameStateController;

    public override void InstallBindings()
    {
        Container.Bind<GameStateController>().FromInstance(_gameStateController).AsSingle();
    }
}

