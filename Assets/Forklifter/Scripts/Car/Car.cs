using Forklifter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] protected Engine _engine;
    [SerializeField] protected Transmission _transmission;

    protected float _horizontalInput;
    protected float _verticalInput;
    protected bool _brake = false;

    private void Update()
    {
        TransmissionHander();
    }

    public void SetMovementInput(Vector2 movement, bool brake)
    {
        _horizontalInput = movement.x;
        _verticalInput = movement.y;
        _brake = brake;
    }

    public void ToggleEngine()
    {
        _engine.ToggleEngine();
    }

    private void TransmissionHander()
    {
        _transmission.TargetMotorDirection = _engine.IsActive.Value ? _verticalInput * _engine.Power.Value : 0f;
        _transmission.TargetSteering = _horizontalInput;
        _transmission.ToggleBrake(_brake);
    }
}
