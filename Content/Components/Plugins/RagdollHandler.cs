using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DirtySnail.Components
{
    public class RagdollHandler : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider _mainCollider;
        [SerializeField] private Rigidbody _mainRigidbody;
        [SerializeField] private Animator _animator;

        private List<Collider> _ragdollCollidersList = new List<Collider>();
        private List<Rigidbody> _ragdollRigidbodiesList = new List<Rigidbody>();

        private void Awake()
        {
            _ragdollCollidersList = GetComponentsInChildren<Collider>().ToList();
            _ragdollRigidbodiesList = GetComponentsInChildren<Rigidbody>().ToList();
        }

        public void SetRagdollActive(bool value)
        {
            if(_mainRigidbody != null)
            {
                _mainRigidbody.isKinematic = value;
            }

            if (_mainCollider != null)
            {
                _mainCollider.enabled = !value;
            }

            if (_animator != null)
            {
                _animator.enabled = !value;
            }

            foreach (Collider item in _ragdollCollidersList)
            {
                if(item != _mainCollider)
                {
                    item.enabled = value;
                }
            }

            foreach (Rigidbody item in _ragdollRigidbodiesList)
            {
                if(item != _mainRigidbody)
                {
                    item.isKinematic = !value;
                }
            }
        }
    }
}


