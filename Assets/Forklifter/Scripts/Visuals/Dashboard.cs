using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace Forklifter
{
    public class Dashboard : MonoBehaviour
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        [SerializeField] private FuelEngine _fuelEngine;
        [SerializeField] private Transmission _transmission;
        [SerializeField] private Scale _motorScale, _fuelScale, _wheel;

        private void Update()
        {
            _motorScale.SetValue(Mathf.Abs(_transmission.CurrentMotorDirection));
            _fuelScale.SetValue(_fuelEngine.FuelAmountPercentage);
            _wheel.SetValue(_transmission.CurrentSteering);
        }
    }
}
