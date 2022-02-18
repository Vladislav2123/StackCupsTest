using UnityEngine;
using Architecture.LevelManager;

public class LastLevelLoader : MonoBehaviour
{
    private void Awake()
    {
        LevelManager.LoadLastLoadedLevel();
    }
}
