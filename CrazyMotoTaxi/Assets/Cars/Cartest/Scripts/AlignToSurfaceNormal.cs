using UnityEngine;

public class AlignToSurfaceNormal : MonoBehaviour
{
    [Header("Surface Detection Values")]
    [SerializeField] float m_rayDistance = 0.1f;
    [SerializeField] LayerMask mask;

    [Header("Object Behavior")]
    [SerializeField] float m_alignSpeed = 5;
    [SerializeField] Transform rotatableGameObject;
    private void Start()
    {
        if(rotatableGameObject == null)
            rotatableGameObject = transform;

        //If LayerMask == nothing
        if(mask ==0)
            mask = 1 << LayerMask.NameToLayer("Default");

    }
    private void FixedUpdate()
    {
        var hit = new RaycastHit();      
        if (Physics.Raycast(transform.position, Vector3.down, out hit, m_rayDistance, mask))
        {
            float desiredAngle = Mathf.Acos(hit.normal.x / hit.normal.y) *Mathf.Rad2Deg;
            rotatableGameObject.localRotation =  Quaternion.LerpUnclamped(rotatableGameObject.transform.localRotation, Quaternion.Euler(-desiredAngle, 0f, 0f), m_alignSpeed*Time.fixedDeltaTime);
        }
    }
}