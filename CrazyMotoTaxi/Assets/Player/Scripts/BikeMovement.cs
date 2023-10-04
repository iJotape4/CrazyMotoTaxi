using UnityEngine;
using MyBox;

public class BikeMovement : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] float currentSpeed = 0f, accelerationSpeed = 10f, decelerationSpeed = 5f, maxSpeed = 50f, brakesForce = 3f;
    [SerializeField, Range(-10f, -0.01f)] float reverseMaxVelocity = -1f;
    Vector2 inputMovement;
    Rigidbody rb;

    [Header ( "Steer ")]
    //[SerializeField, MustBeAssigned] Transform steer;
    public float maxSteerAngle = 30.0f, currentSteerAngle =0f;

    [Header("BikeBody")]
    [SerializeField, MustBeAssigned] Transform bike;
    float maxBikeInclination = 30f, currentBikeInclination =0f;

    [Header("TurnBody")]
    [SerializeField, MustBeAssigned] Transform body;
    private void Awake()
    {
       // rb = GetComponentInChildren<Rigidbody>();
    }

    public void DoMove(Vector2 input)
    {
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
        Debug.DrawRay(transform.position, transform.forward, Color.blue);

        currentSteerAngle = Mathf.Clamp(currentSteerAngle, body.localEulerAngles.y - maxSteerAngle, body.localEulerAngles.y + maxSteerAngle);
        transform.localRotation = Quaternion.Euler(0, currentSteerAngle, 0);

        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }

    public void Accelerate()
    {
        currentSpeed += accelerationSpeed * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }

    public void Reverse()
    {
        if(currentSpeed > 0f) 
        Decelerate(brakesForce);
        else 
        {
            currentSpeed += accelerationSpeed * inputMovement.y * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, reverseMaxVelocity, 0);
        }
    }

    public void Decelerate(float brakesForce =1f)
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0, decelerationSpeed *brakesForce * Time.deltaTime);
    }

    public void Turn()
    {
        if(currentSpeed < 1)
        {
            ReturnBodyToTheirOriginalPos();
            currentSteerAngle += inputMovement.x * Time.deltaTime*50f;

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
        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, currentSteerAngle, transform.localEulerAngles.z);      
    }

    public void ReturnBodyToTheirOriginalPos()
    {
        float inclinationSpeed = 100f; // Adjust this value to control the speed of returning to the original rotation
        currentBikeInclination = Mathf.MoveTowards(currentBikeInclination, 0, inclinationSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -currentBikeInclination);
    }

    public void TurnInverse()
    {
        ReturnSteerToTheirOriginalPos();
        ReturnBodyToTheirOriginalPos();
    }
}