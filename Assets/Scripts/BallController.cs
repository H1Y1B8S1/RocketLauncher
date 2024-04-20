using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private LauncherController launcherController;

    [System.Obsolete]
    private void Start()
    {
        launcherController = FindObjectOfType<LauncherController>();
        StartCoroutine(DestroyAfterLifetime());
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (launcherController != null)
        {
            launcherController.ResetBallStatus();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }
}
