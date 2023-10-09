using UnityEngine;

namespace NPC.Vehicle
{
    [RequireComponent(typeof(Rigidbody))]
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

        /*void FixedUpdate()
        {
         //TODO : Remove Player input
            float throttle = Input.GetAxis("Vertical");
           float steering = Input.GetAxis("Horizontal");

            ApplyAcceleration(throttle);
            ApplySteering(steering);
            ApplyBrakes();
        }*/

        public void ApplyAcceleration(float throttle)
        {
            foreach (WheelAccelerable wheel in driveWheels)
            {
                wheel.Accelerate(throttle * motorTorque);
            }
        }

        public void ApplySteering(float steering)
        {
            foreach (WheelSteerable wheel in steeringWheels)
            {
                wheel.Steer ( steering );
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