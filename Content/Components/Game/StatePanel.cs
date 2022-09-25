using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePanel : MonoBehaviour
{
    [SerializeField] protected GameObject _content;

    public virtual void Activate(StatePanelParamsBase additionalParams = null)
    {

    }
}

public class StatePanelParamsBase
{

}