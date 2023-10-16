using System.Collections;
using UnityEngine;

public class DestroyOnExitPlayerFOV : MonoBehaviour
{
    float timer = 5f;
    Coroutine destroyingCoroutine;

    private void OnBecameInvisible()
    {
        destroyingCoroutine= StartCoroutine(DestroyAfterExitPlayerFOV());
    }

    private void OnBecameVisible()
    {
        if(destroyingCoroutine != null)
        StopCoroutine(destroyingCoroutine);
    }

    IEnumerator DestroyAfterExitPlayerFOV()
    {
        yield return new WaitForSeconds(timer);
        this.transform.root.gameObject.SetActive(false);
    }
}