using UnityEngine;
public abstract class Wheel : MonoBehaviour
{
    protected WheelCollider wcol;
    protected Transform wmesh;

    protected void Start()
    {
        wcol = GetComponentInChildren<WheelCollider>();
        wmesh = GetComponentInChildren<MeshRenderer>().transform;
    }

    protected void FixedUpdate()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        wcol.GetWorldPose(out pos, out rot);
        wmesh.transform.position = pos;
        wmesh.transform.rotation = rot;
    }
}