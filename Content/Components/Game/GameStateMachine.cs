using DirtySnail.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DirtySnail
{
    public enum GameState { Warmup, Gameplay, Fail, Win, Paused }

    public class GameStateMachine : MonoBehaviour
    {
        public string LastStateChangeReason { get; protected set; }

        [SerializeField] protected PlayerBase _playerBase;
        [SerializeField] protected List<StateConfigurables> _stateConfigurablesList = new List<StateConfigurables>();
        [SerializeField] protected LevelProgression _levelProgression;
        [SerializeField] protected float _failDelay;
        [SerializeField] protected float _winDelay;
        [SerializeField] protected float _warmUpDelay;

        protected StateConfigurables _currentStateConfig;

        public virtual void SetState(GameState state, string reason = "")
        {

            if(_currentStateConfig != null)
            {
                if (_currentStateConfig.Type == state)
                    return;
                if (_currentStateConfig.Type == GameState.Win && state == GameState.Fail)
                    return;
                if (_currentStateConfig.Type == GameState.Fail && state == GameState.Win)
                    return;
            }

            LastStateChangeReason = reason;

            _currentStateConfig = _stateConfigurablesList.Find(x => x.Type == state);

            _playerBase.ActionsBlocked = _currentStateConfig.Type != GameState.Gameplay;
            SetStateObjectsActiveStatus();

            if (_currentStateConfig.Type == GameState.Warmup)
            {
                StartCoroutine(WarmupSequence());
            }
            else if(_currentStateConfig.Type == GameState.Fail)
            {
                StartCoroutine(GameFailedSequence());
            }
            else if(_currentStateConfig.Type == GameState.Win)
            {
                StartCoroutine(GameWonSequence());
            }
            else if(_currentStateConfig.Type == GameState.Gameplay)
            {
                _playerBase.StartGameplay();
            }
        }

        protected IEnumerator WarmupSequence()
        {
            _playerBase.Warmup();

            yield return new WaitForSeconds(_warmUpDelay);

            SetState(GameState.Gameplay);
        }

        protected void SetStateObjectsActiveStatus()
        {
            foreach (var item in _currentStateConfig.ObjectsToDisableList)
                item.SetActive(false);

            foreach (var item in _currentStateConfig.ObjectsToEnableList)
                item.SetActive(true);
        }

        protected IEnumerator GameFailedSequence()
        {
            _playerBase.SetFail();

            yield return new WaitForSeconds(_failDelay);

            if (_currentStateConfig.Panel != null)
            {
                FailStateParams failStateParams = new FailStateParams();

                failStateParams.OnRestartButtonClicked = () => RestartLevel();

                yield return new WaitForSeconds(_currentStateConfig.PanelActivationTime);

                _currentStateConfig.Panel.Activate(failStateParams);
            }
        }

        protected IEnumerator GameWonSequence()
        {
            _playerBase.SetWin();

            yield return new WaitForSeconds(_winDelay);

            if (_currentStateConfig.Panel != null)
            {
                WinStateParams winStateParams = new WinStateParams();

                winStateParams.OnRestartButtonClicked = () => RestartLevel();
                winStateParams.OnContinueButtonClicked = () => StartNextLevel();

                yield return new WaitForSeconds(_currentStateConfig.PanelActivationTime);

                _currentStateConfig.Panel.Activate(winStateParams);
            }
        }

        protected virtual void StartNextLevel()
        {
            _levelProgression.LoadNextLevel();
        }

        protected virtual void RestartLevel()
        {
            _levelProgression.RestartLevel();
        }
    }

    [System.Serializable]
    public class StateConfigurables
    {
        [field: SerializeField] public GameState Type { get; private set; }
        [field: SerializeField] public List<GameObject> ObjectsToDisableList { get; private set; }
        [field: SerializeField] public List<GameObject> ObjectsToEnableList { get; private set; }
        [field: SerializeField] public StatePanel Panel { get; private set; }
        [field: SerializeField] public float PanelActivationTime { get; private set; }
    }
}


