using UnityEngine;
using UnityEngine.Events;

public class RadialRaycast : MonoBehaviour
{
    [SerializeField, Range(1f,10f)] private float rayDistance = 3f; 
    private int currentDegree, numberOfRays = 360;
    [SerializeField] private LayerMask rayCastMask;
    [SerializeField] private LayerMask targetLayer;

    public UnityAction<Transform> e_objectFound;

#if UNITY_EDITOR
    [ExecuteAlways]
#endif

    /// <summary>
    /// Draws a radial ray around the object, and sent the hitted elements
    /// </summary>
    public void DrawRadialRay()
    {
        if (currentDegree > 360)
            return;

        RaycastHit hitInfo;
        float angle = currentDegree * 360f / numberOfRays;
        Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

        if (Physics.Raycast(transform.position, direction, out hitInfo, rayDistance, rayCastMask))
        {
            if((1 << hitInfo.collider.gameObject.layer & targetLayer) != 0)
            {
                currentDegree += 20;
                e_objectFound?.Invoke(hitInfo.transform);
            }
        }
        currentDegree++;
        DrawRadialRay();
    }
}