using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Current;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private string _suffix;
    [SerializeField] private string _prefix;

    public UnityEvent<float> ScoreUpdatedEvent;

    private void Awake() => Current = this;

    public float CurrentScore { get; private set; }

    public void AddScore(float score)
    {
        CurrentScore += score;

        CurrentScore = Mathf.Clamp(CurrentScore, 0, int.MaxValue);

        ScoreUpdatedEvent?.Invoke(CurrentScore);

        TryUpdateScoreUI();
    }

    private void TryUpdateScoreUI()
    {
        if (_scoreText != null)
        {
            _scoreText.text = $"{_prefix}{CurrentScore.ToString("F0")}{_suffix}";
        }
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        ScoreUpdatedEvent?.Invoke(0);
        TryUpdateScoreUI();
    }
}
