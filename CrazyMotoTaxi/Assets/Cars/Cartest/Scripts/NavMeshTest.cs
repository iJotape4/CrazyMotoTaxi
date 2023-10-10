using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
    NavMeshAgent agent;
    Camera cam;
    private void Awake()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }
    public float checkDistance = 1.0f;
    public LayerMask obstacleLayer;



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            RaycastHit hit2;
            Vector3 direction = agent.velocity.normalized;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }

            if (Physics.Raycast(transform.position, direction, out hit2, checkDistance, obstacleLayer))
            {
                if (hit2.collider.GetComponent<NavMeshObstacle>())
                    agent.isStopped = true;
                else
                    agent.isStopped = false;

           }
        }
    }
}