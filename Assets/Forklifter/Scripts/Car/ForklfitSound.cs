using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Forklifter
{
    public class ForklfitSound : MonoBehaviour
    {
        CompositeDisposable _disposables = new CompositeDisposable();

        [SerializeField] private Forklift _lift;
        [SerializeField] private AudioSource _sfx;
        
        private float _startVolume;

        private void Start()
        {
            _startVolume = _sfx.volume;
            _lift.Engine.IsActive.Subscribe(engineState => 
            {
                if (engineState)
                {
                    _sfx.Play();
                    _sfx.volume = 0f;
                }
                else
                {
                    _sfx.Stop();
                }
            });
        }

        private void Update()
        {
            _sfx.volume = Mathf.Abs(_lift.CurrentLiftDirection * _startVolume);
        }

        private void OnDestroy()
        {
            _disposables.Clear();
        }
    }
}
