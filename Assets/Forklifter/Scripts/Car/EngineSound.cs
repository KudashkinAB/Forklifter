using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UniRx.Triggers;

namespace Forklifter
{
    public class EngineSound : MonoBehaviour
    {
        CompositeDisposable _disposables = new CompositeDisposable();

        [SerializeField] private Engine _engine;
        [SerializeField] private Transmission _transmission;
        [SerializeField] private AudioSource _loopSFX, _starterSFX, _powerSFX;

        private float _powerVolume;

        private void Start()
        {
            _powerVolume = _powerSFX.volume;
            _engine.IsActive.Subscribe(x => { EngineToggleHandler(x); }).AddTo(_disposables);
            EngineToggleHandler(_engine.IsActive.Value);
        }


        private void Update()
        {
            PowerUpdate();
        }

        public void EngineToggleHandler(bool state)
        {
            if (state)
            {
                _starterSFX.Play();
                _loopSFX.Play();
                _powerSFX.Play();
                PowerUpdate();
            }
            else
            {
                _loopSFX.Stop();
                _powerSFX.Stop();
            }
        }

        public void PowerUpdate()
        {
            if (_engine.IsActive.Value)
            {
                _powerSFX.volume = Mathf.Abs(_transmission.TargetMotorDirection * _powerVolume);
            }
        }

        private void OnDestroy()
        {
            _disposables.Clear();
        }
    }
}
