using UnityEngine;

public class DestroyOnExitPlayerFOV : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        Destroy(this.transform.root.gameObject);
    }
}