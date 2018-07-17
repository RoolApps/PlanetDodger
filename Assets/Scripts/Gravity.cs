using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    // Use this for initialization
    
    public float PullMultiplier;
    public float DistanceMultiplier; // Factor by which the distance affects force

    private GameObject Player;
    private float GravitationalPull; // Pull force
    private float PullRadius; // Radius to pull
    private Vector3 Center;

    private void Start()
    {
        Player = GameObject.Find("Player");
        var radius = transform.localScale.x;
        PullRadius = radius * 2;
        GravitationalPull = radius * radius * PullMultiplier;
        //Center = new Vector3(transform.position.x + renderer., transform.position.y - transform.lossyScale.y / 2, transform.position.z);
    }

    // Function that runs on every physics frame
    void LateUpdate()
    {
        Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            return;
        }
        
        Vector3 direction = GetComponent<SpriteRenderer>().bounds.center - Player.GetComponent<SpriteRenderer>().bounds.center;

        float distance = direction.sqrMagnitude * DistanceMultiplier + 1; 

        rb.AddForce(direction.normalized * (GravitationalPull / distance) * rb.mass * Time.deltaTime);
    }
}
