using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace Forklifter
{
    public class Transmission : MonoBehaviour
    {
        [SerializeField] private WheelCollider[] _wheels;
        [SerializeField] private Transform[] _wheelCorners;
        [SerializeField] private int[] _rotatingWheels;
        [SerializeField] private int[] _motorWheels;
        [SerializeField] private int[] _brakeWheels; 

        [SerializeField] private float _maxSteeringAngle = 30f;
        [SerializeField] private float _motorForce = 50f;
        [SerializeField] private float _brakeForce = 0f;
        [SerializeField] private float _steeringSpeed = 1f;
        [SerializeField] private float _acceleration = 50f;

        public Action<bool> OnBrakeStateChanged;

        public bool IsBraking { get; private set; } = false;
        public float CurrentSteering { get; private set; } = 0f;
        public float CurrentMotorDirection { get; private set; } = 0f;
        
        public float TargetMotorDirection = 0f;
        public float TargetSteering = 0f;

        private float _targetAngle = 0f;

        private void Update()
        {
            UpdateWheels();
        }

        private void FixedUpdate()
        {
            HandleSteering(Time.fixedDeltaTime);
            HandleMotor(Time.fixedDeltaTime);
        }

        public void ToggleBrake(bool state)
        {
            if (IsBraking == state)
                return;
            IsBraking = state;
            OnBrakeStateChanged(state);
            for (int i = 0; i < _brakeWheels.Length; i++)
                _wheels[i].brakeTorque = state ? _brakeForce : 0f;
        }

        private void HandleSteering(float deltaTime)
        {
            if (CurrentSteering == TargetSteering)
                return;
            CurrentSteering = Mathf.MoveTowards(CurrentSteering, TargetSteering, _steeringSpeed * deltaTime);
            _targetAngle = CurrentSteering * _maxSteeringAngle;

            for (int i = 0; i < _rotatingWheels.Length; i++)
                _wheels[i].steerAngle = _targetAngle;
        }

        private void HandleMotor(float deltaTime)
        {
            CurrentMotorDirection = Mathf.MoveTowards(CurrentMotorDirection, TargetMotorDirection, _acceleration * deltaTime);
            for (int i = 0; i < _motorWheels.Length; i++)
                _wheels[i].motorTorque = _motorForce * CurrentMotorDirection;
        }

        private void UpdateWheels()
        {
            for (int i = 0; i < _wheels.Length; i++)
                UpdateWheelPos(_wheels[i], _wheelCorners[i]);
        }

        private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
        {
            Vector3 position;
            Quaternion rotation;
            wheelCollider.GetWorldPose(out position, out rotation);
            trans.rotation = rotation;
            trans.position = position;
        }
    }
}
