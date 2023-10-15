using System.Collections;
using UnityEngine;

namespace City
{
    public class TrafficLightBrain : MonoBehaviour
    {
        [SerializeField] TrafficLight[] lights;
        [SerializeField] float lightDuration =10f;

        private void Awake()
        {
            lights = GetComponentsInChildren<TrafficLight>();
        }

        private void Start()
        {
            StartCoroutine(TrafficLightChangeState());
        }

        IEnumerator TrafficLightChangeState()
        {
            while (true)
            {
                for (int i = 0; i < lights.Length; i++)
                {
                    EnableOnlyOneTrafficLight(i);
                    yield return new WaitForSeconds(lightDuration);
                }
            }
        }

        void EnableOnlyOneTrafficLight(int index)
        {
            for(int i = 0;i < lights.Length;i++)
            {
                if (i == index)
                   StartCoroutine(  lights[i].YellowLight(ENUM_LightColor.Green));
                else
                    StartCoroutine( lights[i].YellowLight(ENUM_LightColor.Red));
            }
        }
    }
}