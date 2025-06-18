using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forklifter
{
    public class Cargo : MonoBehaviour
    {
        [SerializeField] private List<Collider> _colliders = new List<Collider>();
        [SerializeField] private Rigidbody _rb;

        public bool Stored = false;

        public void ToggleInteractive(bool state)
        {
            _rb.isKinematic = !state;
            foreach (var collider in _colliders)
            {
                collider.enabled = state;
            }
        }
    }
}
