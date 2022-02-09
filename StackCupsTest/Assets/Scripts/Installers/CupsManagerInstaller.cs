using UnityEngine;
using Zenject;

public class CupsManagerInstaller : MonoInstaller
{
    [SerializeField] private CupsManager _cupsManager;

    public override void InstallBindings()
    {
        Container.Bind<CupsManager>().FromInstance(_cupsManager).AsSingle();
    }
}