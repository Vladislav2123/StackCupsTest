using UnityEngine;
using Zenject;

public abstract class UIPanel : MonoBehaviour
{
    [Inject] protected GameStateController _gameStateController;

    protected void DisablePanel() => gameObject.SetActive(false);
    protected void EnablePanel() => gameObject.SetActive(true);
}
