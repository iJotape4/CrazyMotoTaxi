using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Credits
{
    public class CreditsManager : MonoBehaviour
    {
        [SerializeField] Scrollbar scrollbar;
        [SerializeField] float scrollVelocity =0.01f;
        [SerializeField] float updateWaitTime =0.1f;
        [SerializeField] TextMeshProUGUI text;
        private void Awake()
        {
            StartCoroutine(MoveScrollCoroutine());
        }

        IEnumerator MoveScrollCoroutine()
        {
            yield return new WaitForSeconds(1f);
            scrollbar.value = 1f;
            text.transform.localScale= Vector3.one;
            while (scrollbar.value >= 0f) 
            { 
                scrollbar.value -= scrollVelocity;
                yield return new WaitForSeconds(updateWaitTime);
            }

            yield return new WaitForSeconds(5f);
            StartCoroutine(MoveScrollCoroutine());
        }
    }
}
