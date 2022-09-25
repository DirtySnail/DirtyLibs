using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonClasses : MonoBehaviour {}

[System.Serializable]
public class MinMaxFloat
{
    public float Min;
    public float Max;

    public float GetRandomValue() => Random.Range(Min, Max);
}

