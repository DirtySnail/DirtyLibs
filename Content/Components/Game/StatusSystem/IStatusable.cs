using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusable
{
    public abstract void AddStatus(StatusBase status);
    public abstract void RemoveStatus(StatusBase status);
}
