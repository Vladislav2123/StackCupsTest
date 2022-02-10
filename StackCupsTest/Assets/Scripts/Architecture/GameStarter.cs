using Architecture.Base;
using Architecture.LevelManager;
using UnityEngine;
using Zenject;

public class GameStarter : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;

    private void Awake()
    {
        if (LevelManager.IsLastLoadedScene()) Bases.Initialize(_diContainer);
        else LevelManager.LoadLastLoadedLevel();
    }

    private void Start() => Bases.OnStart();
}
