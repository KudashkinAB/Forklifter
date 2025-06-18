using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forklifter
{
    public interface IFuelUser
    {
        public float Fuel { get; }
        public void AddFuel(float amount);
    }
}
