using System;
using UnityEngine;

namespace NPC.Vehicle
{
    //[RequireComponent(typeof(Rigidbody))]
    public class VehicleMovement : MonoBehaviour
    {
        [SerializeField] WheelAccelerable[] driveWheels;
        [SerializeField] WheelSteerable[] steeringWheels;
        public float motorTorque = 2000f;


        void Start()
        {
            driveWheels = GetComponentsInChildren<WheelAccelerable>();
            steeringWheels = GetComponentsInChildren<WheelSteerable>();
        }

        public void ApplyAcceleration(float throttle)
        {
            foreach (WheelAccelerable wheel in driveWheels)
            {
                wheel.Accelerate(throttle * motorTorque);
            }
        }

        public void ApplySteering(float steering, bool playerInput)
        {
            foreach (WheelSteerable wheel in steeringWheels)
            {
                wheel.Steer(steering, true);
            }
        }

        public void ApplySteering(float steerAngle)
        {
            foreach (WheelSteerable wheel in steeringWheels)
            {
                wheel.Steer(steerAngle);
            }
        }

        public void ApplyBrakes(bool applyBrakes)
        {
            foreach (WheelAccelerable wheel in driveWheels)
            {
                wheel.ApplyBrakes(applyBrakes);
            }          
        }
        
        public void ApplyBrakes(bool applyBrakes , float brakeTorque)
        {
            foreach (WheelAccelerable wheel in driveWheels)
            {
                wheel.ApplyBrakes(applyBrakes, brakeTorque);
            }          
        }
    }
}