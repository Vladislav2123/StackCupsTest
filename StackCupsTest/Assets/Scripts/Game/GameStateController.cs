using UnityEngine;
using Zenject;

public class GameStateController : MonoBehaviour
{
    public delegate void OnGameStateChange();
    public event OnGameStateChange OnBeforeGameStateEvent;
    public event OnGameStateChange OnGameStateEvent;
    public event OnGameStateChange OnAfterGameStateEvent;

    public bool IsPlaying { get; private set; }
    public bool IsFinished { get; private set; }

    private void Start() => SetBeforeGameState();

    public void SetBeforeGameState()
    {
        OnBeforeGameStateEvent?.Invoke();
        IsPlaying = false;
    }
    public void SetGameState()
    {
        OnGameStateEvent?.Invoke();
        IsPlaying = true;
    }
    public void SetAfterGameState()
    {
        OnAfterGameStateEvent?.Invoke();
        IsPlaying = false;
    }

    public void SetFinished()
    {
        IsFinished = true;
        SetAfterGameState();
    }

    private void OnFinished() => IsFinished = true;
}
