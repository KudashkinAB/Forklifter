using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forklifter
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameObject _firstPerson, _thirdPerson;
        [SerializeField] private FirstPersonController _firstPersonController;

        private bool _fps = true;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void SetRotation(float xRotation, float yRotation, float deltaTime)
        {
            if (_fps)
            {
                _firstPersonController.RotateCamera(xRotation, yRotation, deltaTime);
            }
        }

        public void ToggleFPS()
        {
            _fps = !_fps;
            _firstPerson.SetActive(_fps);
            _thirdPerson.SetActive(!_fps);
        }
    }
}
