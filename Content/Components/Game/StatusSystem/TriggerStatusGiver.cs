using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStatusGiver : MonoBehaviour
{
    [SerializeField] private StatusBase _status;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IStatusable statusable))
        {
            statusable.AddStatus(_status);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IStatusable statusable))
        {
            statusable.RemoveStatus(_status);
        }
    }
}
