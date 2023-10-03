using UnityEngine;
using MyBox;

public class BikeMovement : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] float currentSpeed = 0f, accelerationSpeed = 10f, decelerationSpeed = 5f, maxSpeed = 50f, brakesForce = 3f;
    [SerializeField, Range(-10f, -0.01f)] float reverseMaxVelocity = -1f;
    Vector2 inputMovement;
    Rigidbody rb;

    [Header ( "Steer ")]
    [SerializeField, MustBeAssigned] Transform steer;
    public float maxSteerAngle = 30.0f, currentSteerAngle =0f;

    [Header("Body")]
    [SerializeField, MustBeAssigned] Transform body;
    float maxBodyInclination = 30f, currentBodyInclination =0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void DoMove(Vector2 input)
    {
        inputMovement = input;
    }

    private void Update()
    {
        if (inputMovement.y > 0f)
            Accelerate();
        if (inputMovement.y < 0f)
            Reverse();
        else
            Decelerate();

        transform.position += transform.forward * currentSpeed * Time.deltaTime;

        if (inputMovement.x != 0f)
            Turn();
        else
            TurnInverse();

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
            currentSteerAngle += inputMovement.x * Time.deltaTime*30f;
            currentSteerAngle = Mathf.Clamp(currentSteerAngle, -maxSteerAngle, maxSteerAngle);
            steer.localRotation = Quaternion.Euler(steer.localRotation.x, currentSteerAngle, steer.localRotation.z);
        }
        else
        {
            ReturnSteerToTheirOriginalPos();
            currentBodyInclination += inputMovement.x * Time.deltaTime * 50f;
            currentBodyInclination = Mathf.Clamp(currentBodyInclination, -maxBodyInclination, maxBodyInclination);
            body.localRotation = Quaternion.Euler(body.rotation.x, body.rotation.y, -currentBodyInclination);
        }
    }

    public void ReturnSteerToTheirOriginalPos()
    {
        float steerSpeed = 100f; // Adjust this value to control the speed of returning to the original rotation
        currentSteerAngle = Mathf.MoveTowards(currentSteerAngle, 0, steerSpeed * Time.deltaTime);
        steer.localRotation = Quaternion.Euler(steer.localRotation.x, currentSteerAngle, steer.localRotation.z);      
    }

    public void ReturnBodyToTheirOriginalPos()
    {
        float inclinationSpeed = 100f; // Adjust this value to control the speed of returning to the original rotation
        currentBodyInclination = Mathf.MoveTowards(currentBodyInclination, 0, inclinationSpeed * Time.deltaTime);
        body.localRotation = Quaternion.Euler(body.rotation.x, body.rotation.y, -currentBodyInclination);
    }

    public void TurnInverse()
    {
        ReturnSteerToTheirOriginalPos();
        ReturnBodyToTheirOriginalPos();
    }
}