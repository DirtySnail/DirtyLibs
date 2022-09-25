using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DirtySnail/Status/New Base Status")]
public class StatusBase : ScriptableObject
{
    public string StatusName;
    public string StatusDescription;
    public float StatusAmount;
    public bool IsStatusStackable;
}
