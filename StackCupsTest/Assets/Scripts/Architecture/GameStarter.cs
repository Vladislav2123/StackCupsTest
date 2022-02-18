using Architecture.Base;
using UnityEngine;
using Zenject;

public class GameStarter : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;

    private void Awake() => Bases.Initialize(_diContainer);
    private void Start() => Bases.OnStart();
}
