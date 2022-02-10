using UnityEngine.SceneManagement;
using Architecture.Base;
using UnityEngine;

namespace Architecture.LevelManager
{
    public static class LevelManager
    {
        private const string LAST_LOADED_LEVEL_KEY = "LastLoadedLevel";

        private static void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
            PlayerPrefs.SetInt(LAST_LOADED_LEVEL_KEY, sceneIndex);

            Bases.ResetBase();
        }

        public static void LoadLastLoadedLevel()
        {
            int lastLoadedLevelIndex = PlayerPrefs.GetInt(LAST_LOADED_LEVEL_KEY, 0);
            LoadScene(lastLoadedLevelIndex);
        }

        public static void LoadNextLevel()
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
                LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else LoadScene(0);
        }

        public static int GetCurrentLevel() => SceneManager.GetActiveScene().buildIndex + 1;

        public static bool IsLastLoadedScene() => SceneManager.GetActiveScene().buildIndex == PlayerPrefs.GetInt(LAST_LOADED_LEVEL_KEY, 0);

        public static void ReloadLevel() => LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
