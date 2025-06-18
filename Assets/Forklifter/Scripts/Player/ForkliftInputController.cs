using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Forklifter
{
    public class ForkliftInputController : MonoBehaviour
    {
        private GameInput _gameInput;

        [SerializeField] private Car _car;
        [SerializeField] private Forklift _lift;
        [SerializeField] private CameraController _cameraController;

        private void Awake()
        {
            _gameInput = new GameInput();
            _gameInput.Enable();
            _gameInput.Gameplay.Starter.performed += OnStarterPerformed;
            _gameInput.Gameplay.CameraChange.performed += OnCameraChangerPerformed;
        }

        private void Update()
        {
            _car.SetMovementInput(_gameInput.Gameplay.Movement.ReadValue<Vector2>(), _gameInput.Gameplay.Brake.IsPressed());
            _lift.CurrentLiftDirection = _gameInput.Gameplay.Lift.ReadValue<float>();
            _cameraController.SetRotation(_gameInput.Gameplay.LookX.ReadValue<float>(), _gameInput.Gameplay.LookY.ReadValue<float>(), Time.deltaTime);
        }

        private void OnStarterPerformed(InputAction.CallbackContext context)
        {
            _car.ToggleEngine();
        }

        private void OnCameraChangerPerformed(InputAction.CallbackContext context) 
        {
            _cameraController.ToggleFPS();
        }
    }
}
