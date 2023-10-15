using System.Collections;
using UnityEngine;

namespace City
{
    [RequireComponent(typeof(BoxCollider))]
    public class TrafficLight : MonoBehaviour
    {
        BoxCollider trafficLightCollider;
        [SerializeField] Light greenLight, redLight, yellowLight;

        private void Awake()
        {
            trafficLightCollider = GetComponent<BoxCollider>();
        }

        void ChangeState()
        {
            greenLight.enabled = false;
            redLight.enabled = false;
            yellowLight.enabled = false;
        }

       void GreenLight()
       {
            ChangeState();
            trafficLightCollider.enabled = false;    
            greenLight.enabled = true;
       }

       void RedLight()
       {
            ChangeState();
            trafficLightCollider.enabled = true;
            redLight.enabled = true;
       }

        public IEnumerator YellowLight(ENUM_LightColor lightColor)
        {
            ChangeState();
            trafficLightCollider.enabled =true;
            yellowLight.enabled = true;
            yield return new WaitForSeconds(2f);

            if(lightColor == ENUM_LightColor.Green)
                GreenLight();
            else
                RedLight();
        }

       
    }
    public enum ENUM_LightColor
    {
        Green,
        Red,
        Yellow
    }
}