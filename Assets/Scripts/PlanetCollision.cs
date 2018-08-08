using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCollision : MonoBehaviour {

    public GameObject Explosion;
    public ParticleSystem Smoke;
    public UnityEngine.UI.Button RestartButton;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Planet(Clone)")
        {
            GetComponent<PlayerController>().ScriptEnabled = false;
            Explosion.SetActive(true);
            RestartButton.gameObject.SetActive(true);
            
            //if(Smoke.isStopped)
            //{
            //    Smoke.Play();
            //}
        }
    }
}
