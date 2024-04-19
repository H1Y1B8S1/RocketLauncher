using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    private readonly float ballLifetime = 3f;
    private LauncherController launcherController;

    [System.Obsolete]
    private void Start()
    {
        launcherController = FindObjectOfType<LauncherController>();
        Destroy(gameObject, ballLifetime);
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
        //if (collision.gameObject.CompareTag("Wall") && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    Debug.Log("Wall hit...");
        //}

        if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }

        //Debug.Log(collision.gameObject.name);

    }
}
