using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forklifter
{
    public class Scale : MonoBehaviour
    {
        [SerializeField] private Transform _arrow;
        [SerializeField] private float _value = 0f;
        public float MaxAngle = 90f;
        public float SmoothSpeed = 1f;
        [SerializeField, Range(-1f, 1f)] private float _direction = -1f;

        public bool IsSmooth = true;

        private float _targetValue = 0f;

        private void Awake()
        {
            SetValue(_value, true);
        }

        private void Update()
        {
            if (IsSmooth && _value != _targetValue)
            {
                _value = Mathf.MoveTowards(_value, _targetValue, SmoothSpeed * Time.deltaTime);
                SetAngle(_value);
            }
        }

        public virtual void SetValue(float value, bool forcedFast = false)
        {
            _targetValue = value;
            if(forcedFast || IsSmooth == false)
            {
                _value = value;
                SetAngle(value);
            }
        }

        public void SetAngle(float value)
        {
            _arrow.transform.localEulerAngles
                = new Vector3(_arrow.transform.localEulerAngles.x, _arrow.transform.localEulerAngles.y, value * MaxAngle * _direction);
        }
    }
}
