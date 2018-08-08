using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDestroyer : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Planet(Clone)")
        {
            Destroy(collision.gameObject.GetComponent<Gravity>().ShipParticleSystem.gameObject);
        }
        Destroy(collision.gameObject);
    }
}
