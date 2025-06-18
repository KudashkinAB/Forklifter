using Forklifter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forklift : MonoBehaviour
{
    [SerializeField] private Engine _engine;
    [SerializeField] private Transform _lift;
    [SerializeField] private Transform _root;
    [SerializeField] private float _maxDistance = 2f;
    [SerializeField] private float _speedFullUp = 1f;
    [SerializeField, Range(0f, 1f)] private float _startDistance = 0f;

    public Engine Engine => _engine;

    public float CurrentLiftDirection
    {
        get
        {
            return _currentLiftDirection;
        }
        set
        {
            if (_engine.IsActive.Value)
                _currentLiftDirection = value;
            else
                _currentLiftDirection = 0f;
        }
    }

    private float _currentLiftDistance = 0f;
    private float _currentLiftDirection = 0f;

    private void Start()
    {
        SetDistance(_startDistance);
    }

    private void Update()
    {
        if (_engine.IsActive.Value == false)
            return;

        MoveLift(CurrentLiftDirection, Time.deltaTime);
    }

    public void MoveLift(float direction, float deltaTime)
    {
        SetDistance(_currentLiftDistance + direction * _speedFullUp * deltaTime);
    }

    public void SetDistance(float distance)
    {
        distance = Mathf.Clamp(distance, 0f, 1f);
        _currentLiftDistance = distance;
        _lift.position = _root.position + _root.up * distance * _maxDistance;
    }
}
