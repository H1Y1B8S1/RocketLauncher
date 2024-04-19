using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float launchForce = 10f;

    private bool isBallActive = false;
    private Vector2 touchStartPosition;

    private void Awake()
    {
        isBallActive = false; // Ensure the flag starts as false
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
                Debug.Log(touchStartPosition);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 touchEndPosition = touch.position;
                Debug.Log(touchEndPosition);
                Vector2 touchDirection = (touchEndPosition - touchStartPosition).normalized;

                LaunchBall(touchDirection);
                Debug.Log(touchDirection);
            }
        }
    }

    private void LaunchBall(Vector2 launchDirection)
    {
        if (!isBallActive)
        {
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            Rigidbody ballRB = ball.GetComponent<Rigidbody>();

            ballRB.velocity = launchDirection * launchForce;
            isBallActive = true;
        }
    }

    public void ResetBallStatus()
    {
        isBallActive = false;
    }
}
  