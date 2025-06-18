using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forklifter
{
    public class GasStation : MonoBehaviour
    {
        [SerializeField] private float _fillSpeed = 30f;

        private IFuelUser _currentUser;

        private void Update()
        {
            if (_currentUser != null)
                _currentUser.AddFuel(_fillSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_currentUser == null && other.gameObject.TryGetComponent(out IFuelUser fuelUser))
            {
                _currentUser = fuelUser;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(_currentUser != null && other.gameObject.TryGetComponent(out IFuelUser fuelUser) && fuelUser == _currentUser)
            {
                _currentUser = null;
            }
        }
    }
}
