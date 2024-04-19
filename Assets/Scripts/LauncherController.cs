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
                Debug.Log(touchStartPosition);
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                UpdateDirectionLine(touch.position);
                Debug.Log(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 touchEndPosition = touch.position;
                Vector2 touchDelta = touchEndPosition - touchStartPosition;
                Debug.Log(touchDelta);

                if (touchDelta.sqrMagnitude >= minTouchMagnitude * minTouchMagnitude)
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
        Vector3 launchDirection = (touchWorldPosition - transform.position).normalized;
        Vector3 lineEndPosition = transform.position + launchDirection * directionLineSize; // Adjust the multiplier (3f) for desired line length

        Vector3[] linePositions = { transform.position, lineEndPosition };
        directionLine.positionCount = 2;
        directionLine.SetPositions(linePositions);
        directionLine.enabled = true;
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
        }
    }

    public void ResetBallStatus()
    {
        isBallActive = false;
    }
}
  