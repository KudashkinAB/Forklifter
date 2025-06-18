using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forklifter
{
    public class UiFade : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _time;
        [SerializeField] private bool _playOnAwake = false;

        void Start()
        {
            if( _playOnAwake )
                Fade(false);
        }

        public void Fade(bool state)
        {
            _canvasGroup.DOFade(state ? 1f : 0f, _time);
        }
    }
}
