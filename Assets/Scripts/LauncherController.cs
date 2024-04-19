using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float launchForce = 10f;

    private bool isBallActive = false;
    private Vector2 touchStartPosition;
    [SerializeField] float minTouchMagnitude = 10f;

    [SerializeField] LineRenderer directionLine;
    [SerializeField] float directionLineSize = 10f;


    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
                UpdateDirectionLine(touchStartPosition);
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                UpdateDirectionLine(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 touchEndPosition = touch.position;
                Vector2 touchDelta = touchEndPosition - new Vector2(transform.position.x,transform.position.y);

                // (touchDelta.sqrMagnitude >= minTouchMagnitude * minTouchMagnitude)
                {
                    Vector2 touchDirection = touchDelta.normalized;
                    LaunchBall(touchDirection);
                }

                ClearDirectionLine();
            }
        }
        else
        {
            ClearDirectionLine();
        }
    }

    private void UpdateDirectionLine(Vector2 endPosition)
    {
        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(endPosition.x, endPosition.y, 10f));
        Vector2 launchDirection = ((Vector2)touchWorldPosition - (Vector2)transform.position).normalized;
        Vector2 lineEndPosition = (Vector2)transform.position + launchDirection * directionLineSize;

        Vector3[] linePositions = { transform.position, lineEndPosition };
        directionLine.positionCount = 2;
        directionLine.SetPositions(linePositions);
        directionLine.enabled = true;

        // Debug information
       // Debug.DrawRay(transform.position, (Vector3)launchDirection, Color.green);
        //Debug.Log("Line Direction: " + launchDirection);
    }



    private void ClearDirectionLine()
    {
        directionLine.positionCount = 0;
    }


    private void LaunchBall(Vector2 launchDirection)
    {
        if (!isBallActive)
        {
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            Rigidbody ballRB = ball.GetComponent<Rigidbody>();

            ballRB.velocity = launchDirection * launchForce;
            isBallActive = true;

            // Debug information
            //Debug.DrawRay(transform.position, launchDirection * 2f, Color.blue); // Draw a debug ray in the launch direction for the ball
            Debug.Log("Ball Launch Direction: " + launchDirection); // Print the ball launch direction to the console
        }
    }


    public void ResetBallStatus()
    {
        isBallActive = false;
    }
}
  