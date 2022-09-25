using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomizer : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsToRandomize = new List<GameObject>();

    private void Awake()
    {
        if (_objectsToRandomize.Count == 0)
            return;

        int randIndex = Random.Range(0, _objectsToRandomize.Count);

        for (int i = 0; i < _objectsToRandomize.Count; i++)
        {
            _objectsToRandomize[i].SetActive(i == randIndex);
        }
    }
}
