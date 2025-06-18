using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Forklifter
{
    public abstract class Engine : MonoBehaviour
    {
        public ReactiveProperty<bool> IsActive { get; protected set; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<float> Power { get; protected set; } = new ReactiveProperty<float>(1f);

        public virtual void ToggleEngine()
        {
            ToggleEngine(!IsActive.Value);
        }

        public virtual void ToggleEngine(bool state)
        {
            IsActive.Value = state;
        }

        public virtual bool IsEngineActive()
        {
            return IsActive.Value;
        }

        public virtual float GetEngineMaxPower()
        {
            return 1f;
        }
    }
}
