using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCleanup : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsToDestroyList = new List<GameObject>();
    [SerializeField] private string _tagToCheck;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(_tagToCheck))
        {
            foreach (var item in _objectsToDestroyList)
            {
                Destroy(item.gameObject);
            }

            _objectsToDestroyList.Clear();
        }
    }
}
