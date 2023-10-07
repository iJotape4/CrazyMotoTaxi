using MyBox;
using UnityEngine;

public class BikeMovementPhyisic : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] float currentSpeed = 0f, accelerationSpeed = 10f, decelerationSpeed = 5f, maxSpeed = 50f, brakesForce = 3f;
    [SerializeField, Range(-10f, -0.01f)] float reverseMaxVelocity = -1f;
    Vector2 inputMovement;
    Rigidbody rb;

    [Header("Steer ")]
    [SerializeField, MustBeAssigned] Transform steer;
    public float maxSteerAngle = 30.0f, currentSteerAngle = 0f;

    [Header("BikeBody")]
    [SerializeField, MustBeAssigned] Transform bike;
    float maxBikeInclination = 30f, currentBikeInclination = 0f;

    [Header("TurnBody")]
    [SerializeField, MustBeAssigned] Transform body;
    private void Awake()
    {
        // rb = GetComponentInChildren<Rigidbody>();
    }

    public void DoMove(Vector2 input)
    {
        Debug.Log(input);
        inputMovement = input;


    }

    private void FixedUpdate()
    {
        if (inputMovement.y > 0f)
            Accelerate();
        if (inputMovement.y < 0f)
            Reverse();
        else
            Decelerate();



        if (inputMovement.x != 0f)
            Turn();
        else
            TurnInverse();

        Debug.DrawRay(transform.position, steer.forward, Color.blue);
        Debug.DrawRay(transform.position, body.forward, Color.yellow);
        Debug.DrawRay(transform.position, (steer.forward + body.forward) / 2, Color.black);

        currentSteerAngle = Mathf.Clamp(currentSteerAngle, - maxSteerAngle,  maxSteerAngle);
        steer.localEulerAngles = new Vector3(0, currentSteerAngle, 0);

    }

    public void Accelerate()
    {
        if(body.forward != steer.forward)
        {
            body.localEulerAngles = new Vector3(body.localEulerAngles.x,  Mathf.LerpAngle(body.localEulerAngles.y, steer.eulerAngles.y, Time.deltaTime), body.localEulerAngles.z);
        }
        currentSpeed += accelerationSpeed * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        transform.position += steer.forward * currentSpeed * Time.deltaTime;

    }

    public void Reverse()
    {
        if (currentSpeed > 0f)
            Decelerate(brakesForce);
        else
        {
            if (body.forward != steer.forward)
            {
                body.localEulerAngles = new Vector3(body.localEulerAngles.x, Mathf.LerpAngle( body.localEulerAngles.y, -steer.eulerAngles.y, Time.deltaTime), body.localEulerAngles.z);
            }
            currentSpeed += accelerationSpeed * inputMovement.y * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, reverseMaxVelocity, 0);

            transform.position += (steer.forward + body.forward)/2* currentSpeed * Time.deltaTime;
        }
    }

    public void Decelerate(float brakesForce = 1f)
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0, decelerationSpeed * brakesForce * Time.deltaTime);
    }

    public void Turn()
    {
        if (currentSpeed < 1)
        {
            ReturnBodyToTheirOriginalPos();
            currentSteerAngle += inputMovement.x * Time.deltaTime * 50f;

        }
        /* else
         {
             currentBikeInclination += inputMovement.x * Time.deltaTime * 50f;
             currentBikeInclination = Mathf.Clamp(currentBikeInclination, body.localEulerAngles.z - maxBikeInclination, body.localEulerAngles.z +maxBikeInclination);

             if(currentSpeed!=0)
                 transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -currentBikeInclination);
         }*/

    }

    public void ReturnSteerToTheirOriginalPos()
    {
        float steerSpeed = 100f; // Adjust this value to control the speed of returning to the original rotation
        currentSteerAngle = Mathf.MoveTowards(currentSteerAngle, 0, steerSpeed * Time.deltaTime);
        steer.localRotation = Quaternion.Euler(steer.localEulerAngles.x, currentSteerAngle, steer.localEulerAngles.z);
    }

    private void ReturnBodyToTheirOriginalPos()
    {
        float inclinationSpeed = 100f; // Adjust this value to control the speed of returning to the original rotation
        currentBikeInclination = Mathf.MoveTowards(currentBikeInclination, 0, inclinationSpeed * Time.deltaTime);
        body.localRotation = Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, -currentBikeInclination);
    }

    public void TurnInverse()
    {
        ReturnSteerToTheirOriginalPos();
        ReturnBodyToTheirOriginalPos();
    }
}