using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forklifter
{
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _spine;
        [SerializeField] private float _yAxisClamp = 80f;
        [SerializeField] private Vector2 _sensivity = new Vector2(15f, 7f);

        private float _camXAngle = 0f;

        public void RotateCamera(float rotX, float rotY, float deltaTime)
        {
            rotX *= _sensivity.x;
            rotY *= _sensivity.y;
            _camXAngle -= rotY * deltaTime;
            _camXAngle = Mathf.Clamp(_camXAngle, -_yAxisClamp, _yAxisClamp);
            _spine.Rotate(0, rotX * deltaTime, 0, Space.Self);
            _cameraTransform.localEulerAngles = new Vector3(_camXAngle, _cameraTransform.localEulerAngles.y, _cameraTransform.localEulerAngles.z);
        }
    }
}
