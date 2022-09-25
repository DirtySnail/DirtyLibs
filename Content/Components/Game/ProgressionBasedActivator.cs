using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirtySnail.Components
{
    public enum ProgressionActivateType { Odd, Even, Range}

    public class ProgressionBasedActivator : MonoBehaviour
    {
        [SerializeField] private ProgressionActivateType _activationType;
        [SerializeField] private List<ProgressionRange> _availableRangesList = new List<ProgressionRange>();
        [SerializeField] private List<GameObject> _objectsToActivateList = new List<GameObject>();
        [SerializeField] private LevelProgression _levelProgression;

        private void Awake()
        {
            if(_levelProgression == null)
            {
                _levelProgression = FindObjectOfType<LevelProgression>();
            }
        }

        private void Start()
        {
            if(_activationType == ProgressionActivateType.Range)
            {
                foreach (var item in _objectsToActivateList)
                {
                    item.SetActive(_availableRangesList.Find(x => x.Contains(_levelProgression.GetPlayerProgressionLevel())) != null);
                }
            }
            else if(_activationType == ProgressionActivateType.Odd)
            {
                foreach (var item in _objectsToActivateList)
                {
                    item.SetActive(_levelProgression.GetPlayerProgressionLevel() % 2 == 1);
                }
            }
            else if(_activationType == ProgressionActivateType.Even)
            {
                foreach (var item in _objectsToActivateList)
                {
                    item.SetActive(_levelProgression.GetPlayerProgressionLevel() % 2 == 0);
                }
            }
        }
    }
}

[System.Serializable]
public class ProgressionRange
{
    public int MinLevel;
    public int MaxLevel;

    public bool Contains(int value)
    {
        return (value >= MinLevel && value <= MaxLevel);
    }
}

[System.Serializable]
public class FloatRange
{
    public float Min;
    public float Max;

    public float GetRandomValue() => Random.Range(Min, Max);
}
