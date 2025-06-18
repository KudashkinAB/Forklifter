using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Forklifter
{
    public class FuelEngine : Engine, IFuelUser
    {
        [SerializeField] protected float _fuelMaxCapacity = 100f;
        [SerializeField] protected float _consumptionModifier = 1f;
        [SerializeField] private AnimationCurve _powerFuelCurve;

        public ReactiveProperty<float> CurrentFuel { get; protected set; }

        public float FuelAmountPercentage => CurrentFuel.Value / _fuelMaxCapacity;

        public float Fuel 
        { 
            get
            {
                return CurrentFuel.Value;
            } 
        }

        private void Awake()
        {
            CurrentFuel = new ReactiveProperty<float>(1f);
        }

        private void Start()
        {
            CurrentFuel = new ReactiveProperty<float>(_fuelMaxCapacity);
        }

        private void Update()
        {
            if (IsActive.Value)
            {
                AddFuel(-_consumptionModifier * Time.deltaTime);
                Power.Value = _powerFuelCurve.Evaluate(1f - FuelAmountPercentage);
                if(CurrentFuel.Value <= 0)
                {
                    IsActive.Value = false;
                }
            }
            //Debug.Log($"Engine: {IsActive.Value} | Fuel: {CurrentFuel.Value}");
        }

        public override void ToggleEngine(bool state)
        {
            if (state)
            {
                if (CurrentFuel.Value > 0f)
                {
                    IsActive.Value = true;
                }
            }
            else
            {
                IsActive.Value = false;       
            }
        }

        public void AddFuel(float amount)
        {
            CurrentFuel.Value = Mathf.Clamp(CurrentFuel.Value + amount, 0f, _fuelMaxCapacity);
        }
    }
}
