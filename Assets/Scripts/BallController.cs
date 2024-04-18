using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    [SerializeField] float ballLifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, ballLifetime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("Wall hit...");
        }

        if (collision.gameObject.CompareTag("Bottom"))
        {
            Destroy(gameObject);
        }
    }
}
