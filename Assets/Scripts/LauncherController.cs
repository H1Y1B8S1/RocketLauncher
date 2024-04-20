using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float launchForce = 10f;

    private bool isBallActive = false;
   // [SerializeField] float minTouchMagnitude = 10f;

    [SerializeField] LineRenderer directionLine;
    [SerializeField] float directionLineSize = 10f;

    private Vector2 touchStartPosition;
    private Vector2 launcherWorldPosition;

    private void Start()
    {
        launcherWorldPosition = transform.position;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                UpdateDirectionLine(touchStartPosition);
                touchStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                UpdateDirectionLine(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 touchEndPosition = touch.position;
                Vector2 touchEndWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchEndPosition.x, touchEndPosition.y, 0f));

                Vector2 touchDirection = (touchEndWorldPosition - launcherWorldPosition).normalized;

                //Debug.Log("launcherWorldPosition: " + launcherWorldPosition);
                //Debug.Log("touchEndPosition: " + touchEndPosition);
                //Debug.Log("touchEndWorldPosition: " + touchEndWorldPosition);
                //Debug.Log("touchDirection: " + touchDirection);

                LaunchBall(touchDirection);
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
        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(endPosition.x, endPosition.y, 0f));
        Vector2 launchDirection = ((Vector2)touchWorldPosition - (Vector2)transform.position).normalized;
        Vector2 lineEndPosition = (Vector2)transform.position + launchDirection * directionLineSize;

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

            ballRB.velocity = (Vector3)launchDirection * launchForce;
            isBallActive = true;
        }
    }

 



    public void ResetBallStatus()
    {
        isBallActive = false;
    }
}
  