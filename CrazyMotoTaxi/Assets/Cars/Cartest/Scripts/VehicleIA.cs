using MyBox;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NPC.Vehicle
{
    [RequireComponent(typeof(VehicleMovement))]
    public class VehicleIA : MonoBehaviour
    {
        NavMeshHit frontRay, leftRay, rightRay,backRay;
        Vector3 frontDirection, rightDirection, leftDirection, superRightDirection, superLeftDirection;
        VehicleMovement vehicleMovement;
        [SerializeField, ReadOnly] float brakeDistance = 4f;
        [SerializeField, Range(0.01f , 1f)]  float brakeDistanceCalcMultiplier=0.32f;
        [SerializeField] Vector3 yOffset = new Vector3(0f, 0.5f, 0f);
        // Start is called before the first frame update
        private bool isblockedFront, isBlockedLeft, isBlockedRight = false;
        float currentSteer=0;
        Rigidbody rb;
        [SerializeField] float allowedDistance;
        [SerializeField, Range(1f, 500f)] float customBrakeTorque = 50f;

        delegate void CheckDirection();

        int areaMask;
        private void Awake()
        {
            areaMask =1 << NavMesh.GetAreaFromName("Not Walkable") ;
        }

        void Start()
        {
            vehicleMovement = GetComponent<VehicleMovement>();
            rb= GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {      
            frontDirection = transform.position +(transform.forward * brakeDistance);
            rightDirection = transform.position + (transform.forward + transform.right);
            leftDirection = transform.position + (transform.forward - transform.right );
            superRightDirection = transform.position + transform.right*2;
            superLeftDirection = transform.position - transform.right *2;

            Debug.DrawLine(transform.position, frontDirection, isblockedFront ? Color.red : Color.green);
            Debug.DrawLine(transform.position, rightDirection, isBlockedRight ? Color.red : Color.green);
            Debug.DrawLine(transform.position, leftDirection, isBlockedLeft ? Color.red : Color.green);
            Debug.DrawLine(transform.position, superRightDirection, isBlockedRight ? Color.red : Color.green);
            Debug.DrawLine(transform.position, superLeftDirection, isBlockedLeft ? Color.red : Color.green);

            CheckGoFront();
           // CheckTurnRight();
            //CheckTurnLeft();
                         
        }

        void CheckGoFront()
        {
            isblockedFront = NavMesh.Raycast(transform.position, frontDirection, out frontRay, NavMesh.AllAreas);

            if (isblockedFront && isBlockedLeft && isBlockedRight)
            {
                Reverse();
                return;
            }

            if (isblockedFront)
            {
                vehicleMovement.ApplyBrakes(true, customBrakeTorque);
                CheckDirection d = Random.Range(0, 2) switch
                {
                    0 => CheckTurnRight,
                    1 => CheckTurnLeft,
                    _ => CheckGoFront
                };
                d();
                Accelerate();
            }
            else
            {
                //StartCoroutine(StraightenSteering());
                StraigthSteer();
                Accelerate();
            }
        }

        void Accelerate()
        {
            CalculateBrakeDistance();
            vehicleMovement.ApplyAcceleration(10f);
            vehicleMovement.ApplyBrakes(false);
        }

        void Reverse()
        {
            vehicleMovement.ApplyAcceleration(-100f);
            vehicleMovement.ApplyBrakes(false);
            isBlockedLeft = false; isBlockedRight=false;
        }

        void CheckTurnRight()
        {
           NavMesh.Raycast(transform.position, superRightDirection, out rightRay, NavMesh.AllAreas);
            isBlockedRight = Vector3.Distance(rightRay.position, transform.position) < allowedDistance;
            if (isBlockedRight)
            {
                currentSteer -= 1f;
                currentSteer = Mathf.Clamp(currentSteer, -1f, 1f);
                vehicleMovement.ApplySteering(currentSteer);
                Accelerate();
            }
            /*if (canTurnRight)
            {
               
                Accelerate();
            }
            else
            {
                StraigthSteer();
            }*/
        }  
        void CheckTurnLeft()
        {
           NavMesh.Raycast(transform.position, superLeftDirection, out leftRay, NavMesh.AllAreas);
            isBlockedLeft = Vector3.Distance(leftRay.position, transform.position) < allowedDistance;
            if (isBlockedLeft)
            {
                currentSteer += 1f;
                currentSteer = Mathf.Clamp(currentSteer, -1f, 1f);
                vehicleMovement.ApplySteering(currentSteer);
                Accelerate();
            }

            //if (canTurnLeft)
            //{
            //    currentSteer -= Mathf.Clamp(1f, -1f, 1f);
            //    vehicleMovement.ApplySteering(currentSteer);
            //    Accelerate();
            //}
            //else
            //{
            //    StraigthSteer();
            //}
        }


        IEnumerator StraightenSteering()
        {
            //float randomTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(2f);
            currentSteer *=0;
            vehicleMovement.ApplySteering(currentSteer);
        }

        void StraigthSteer()
        {
            currentSteer *= 0;
            vehicleMovement.ApplySteering(currentSteer);
        }

        void CalculateBrakeDistance()
        {
            float newDistance = Mathf.Pow(rb.velocity.z, 2) * brakeDistanceCalcMultiplier;
            if (newDistance <= 4f)
                brakeDistance = 4f;
            else
                brakeDistance = newDistance;
        }      
    }
}