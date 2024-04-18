using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherController : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float launchForce = 10f;

    private Rigidbody ballRB;

    private void Awake()
    {
        ballRB = ballPrefab.GetComponent<Rigidbody>();
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

    public void LaunchBall()
    {
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        ballRB.velocity = Vector2.up * launchForce;

    }
}
  