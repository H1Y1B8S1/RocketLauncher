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
        if (!collision.gameObject.CompareTag("Brick")) // Assuming "Brick" is the tag for your brick GameObjects
        {
            if (collision.gameObject.CompareTag("Finish"))
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        Vector3 ballDirection = GetComponent<Rigidbody>().velocity.normalized;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, new Vector3(ballDirection.x, ballDirection.y, 0f), Mathf.Infinity);

        StartCoroutine(DestroyBricksWithDelay(hits));

    }

    private IEnumerator DestroyBricksWithDelay(RaycastHit[] hits)
    {
        float delayBetweenBrickDestroy = 0.5f; // Adjust the delay time as needed

        int brickCount = 0;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Brick")) // Check if collider is not null and has the "Brick" tag
            {
                yield return new WaitForSeconds(delayBetweenBrickDestroy);
                if (hit.collider != null && hit.collider.gameObject != null) // Check again before destroying to avoid null reference
                {
                    Destroy(hit.collider.gameObject);
                    brickCount++;
                    delayBetweenBrickDestroy = 0.01f;
                }
            }
        }

        Debug.Log("Total Bricks Hit: " + brickCount);
    }

}
