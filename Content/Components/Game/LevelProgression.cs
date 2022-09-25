using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DirtySnail.Components
{
    public class LevelProgression : MonoBehaviour
    {
        [SerializeField] private string _gameLevelPrefix;
        [SerializeField] private string _gameName;
        [SerializeField] private int _maxAvailableLevels;

        private bool _actionReceived;

        public void LoadNextLevel()
        {
            if (_actionReceived)
                return;

            _actionReceived = true;

            IncreasePlayerLevel();
            LoadCurrentLevel();
        }

        public void RestartLevel()
        {
            if (_actionReceived)
                return;

            _actionReceived = true;

            LoadCurrentLevel();
        }

        private void LoadCurrentLevel()
        {
            int playerLevel = GetPlayerProgressionLevel();
            int levelToStart = playerLevel;

            if (playerLevel > _maxAvailableLevels)
            {
                levelToStart = playerLevel % _maxAvailableLevels;

                if (levelToStart == 0)
                {
                    levelToStart = _maxAvailableLevels;
                }
            }

            SceneManager.LoadScene($"{_gameLevelPrefix}{levelToStart}");
        }

        public int GetPlayerProgressionLevel()
        {
            return PlayerPrefs.GetInt($"{_gameName}", 1);
        }

        private void IncreasePlayerLevel()
        {
            PlayerPrefs.SetInt(_gameName, GetPlayerProgressionLevel() + 1);
        }
    }
}
