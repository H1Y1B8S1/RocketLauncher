using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float launchForce = 10f;

    private bool isBallActive = false;

    private void Awake()
    {
        isBallActive = false; // Ensure the flag starts as false
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                LaunchBall();
            }
        }
    }

    private void LaunchBall()
    {
        if (!isBallActive)
        {
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            Rigidbody ballRB = ball.GetComponent<Rigidbody>();

            ballRB.velocity = Vector2.up * launchForce;
            isBallActive = true;
        }
    }

    public void ResetBallStatus()
    {
        isBallActive = false;
    }
}
  