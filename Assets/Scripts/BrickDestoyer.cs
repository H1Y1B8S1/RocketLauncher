using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDestoyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Debug.Log("Collision");
            Destroy(collision.gameObject);
        }
    }
}
