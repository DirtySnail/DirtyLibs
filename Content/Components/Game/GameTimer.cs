using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DirtySnail.Components
{
    public class GameTimer : MonoBehaviour
    {
        public System.Action OnCompleteEvent;

        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private string _timerPrefix;
        [SerializeField] private string _timerSuffix;

        private bool _isActive;
        private float _currentTimer;

        public void SetTimerActive(bool value) => _isActive = value;

        public void SetTimer(float value)
        {
            _currentTimer = value;
            UpdateTimerUI();
        }

        public float GetValue() => _currentTimer;

        private void Update()
        {
            if (!_isActive)
                return;

            if(_currentTimer > 0f)
            {
                _currentTimer -= Time.deltaTime;

                UpdateTimerUI();
                CheckTimer();
            }
        }

        private void UpdateTimerUI()
        {
            if (_timerText != null)
            {
                _timerText.text = $"{_timerPrefix}{_currentTimer.ToString("F1")}{_timerSuffix}";
            }
        }

        public void DeductTimer(float value)
        {
            _currentTimer -= value;

            _currentTimer = Mathf.Clamp(_currentTimer, 0, Mathf.Infinity);

            CheckTimer();
        }

        private void CheckTimer()
        {
            if(_currentTimer <= 0)
            {
                OnCompleteEvent?.Invoke();
                _isActive = false;
            }
        }
    }
}