using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            Effects.Current.ShakeCamera();
            Destroy(gameObject.transform.parent.gameObject, 1);
        }
    }
}
