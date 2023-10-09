using UnityEngine;

namespace NPC.Vehicle
{
    public class WheelAccelerable : Wheel, IAccelerable, IBrakeable
    {
        [SerializeField] protected float brakeTorque = 500f;

        public void Accelerate(float powerInput)
        {
            wcol.motorTorque = powerInput;
        }

       public void ApplyBrakes(bool brake)
       {
            if (brake) 
                wcol.brakeTorque = brakeTorque;
            else
                wcol.brakeTorque = 0;
       } 
        
        public void ApplyBrakes(bool brake, float _brakeTorque)
        {
            if (brake) 
                wcol.brakeTorque = _brakeTorque;
            else
                wcol.brakeTorque = 0;
        }
    }
}