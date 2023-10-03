using UnityEngine;

public class BikeMovement : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] float currentSpeed = 0f, accelerationSpeed = 10f, decelerationSpeed = 5f, maxSpeed = 50f, brakesForce = 3f;
    [SerializeField, Range(-10f, -0.01f)] float reverseMaxVelocity = -1f;
    Vector2 inputMovement;
    Rigidbody rb;

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
}