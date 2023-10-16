using UnityEngine;

namespace City
{
    public class PlayerPositionNotifier : MonoBehaviour
    {
        public static Vector3 playerPosition;
        void Update()
        {
            playerPosition = transform.position;
        }
    }
}