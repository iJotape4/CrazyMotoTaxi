using UnityEngine;

public class SteerController : MonoBehaviour
{
    [SerializeField] float currentSteerAngle=0;
    public void TurnSteer(Vector2 turnAmount)
    {
        currentSteerAngle += turnAmount.y;
        transform.localRotation = Quaternion.Euler(0, currentSteerAngle, 0);
        Debug.Log("Turning");
    }
}
