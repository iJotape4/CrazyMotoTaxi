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

        void FixedUpdate()
        {
         //TODO : Remove Player input
            float throttle = Input.GetAxis("Vertical");
           float steering = Input.GetAxis("Horizontal");

            ApplyAcceleration(throttle);
            ApplySteering(steering);
            ApplyBrakes();
        }

        void ApplyAcceleration(float throttle)
        {
            foreach (WheelAccelerable wheel in driveWheels)
            {
                wheel.Accelerate(throttle * motorTorque);
            }
        }

        void ApplySteering(float steering)
        {
            foreach (WheelSteerable wheel in steeringWheels)
            {
                wheel.Steer ( steering );
            }
        }

        void ApplyBrakes()
        {
            //TODO : Remove Player input
            if (Input.GetKey(KeyCode.Space))
            {
                foreach (WheelAccelerable wheel in driveWheels)
                {
                    wheel.ApplyBrakes(true);
                }
            }
            else
            {
                foreach (WheelAccelerable wheel in driveWheels)
                {
                    wheel.ApplyBrakes(false);
                }
            }
        }
    }
}
