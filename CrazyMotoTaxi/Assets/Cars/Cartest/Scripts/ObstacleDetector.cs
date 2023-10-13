using MyBox;
using UnityEngine;
using UnityEngine.AI;

public class ObstacleDetector : MonoBehaviour
{
    [SerializeField, ReadOnly] float brakeDistance = 30f;
    [SerializeField] LayerMask mask;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();   
    }

    private void FixedUpdate()
    {
        CalculateBrakeDistance();
    }

    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward ,brakeDistance,mask))       
            StopNavMeshAgent(true);       
        else       
            StopNavMeshAgent(false);
        
        //Debug.DrawRay(transform.position, transform.forward * brakeDistance, Color.yellow, 0.1f);
    }

    void StopNavMeshAgent(bool stop)
    {
        agent.isStopped = stop;
    }

    void CalculateBrakeDistance()
    {
        float newDistance = Mathf.Pow(agent.velocity.magnitude, 2);
        if (newDistance <= 5f)
            brakeDistance = 5f;
        else
            brakeDistance = newDistance;
    }
}