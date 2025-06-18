using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forklifter
{
    public class BrakeLights : MonoBehaviour
    {
        [SerializeField] private GameObject _lights;
        [SerializeField] private Transmission _transmission;

        private void Start()
        {
            _transmission.OnBrakeStateChanged += ToggleLights;
        }

        private void OnDestroy()
        {
            _transmission.OnBrakeStateChanged -= ToggleLights;
        }

        private void ToggleLights(bool state)
        {
            _lights.SetActive(state);
        }
    }
}
