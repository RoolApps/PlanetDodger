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
    private Rigidbody2D PlayerRigidbody;
    private SpriteRenderer PlayerSpriteRenderer;
    private SpriteRenderer PlanetSpriteRenderer;

    private void Start()
    {
        var player = GameObject.Find("Player");
        PlayerRigidbody = player.GetComponent<Rigidbody2D>();
        PlayerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        PlanetSpriteRenderer = GetComponent<SpriteRenderer>();

        var radius = PlanetSpriteRenderer.bounds.size.x / 2;
        PullRadius = radius * 4;
        GravitationalPull = radius * radius * PullMultiplier;
    }

    // Function that runs on every physics frame
    void FixedUpdate()
    {
        Vector3 direction = PlanetSpriteRenderer.bounds.center - PlayerSpriteRenderer.bounds.center;
        if (direction.magnitude > PullRadius)
        {
            return;
        }

        float distance = direction.sqrMagnitude * DistanceMultiplier + 1;

        PlayerRigidbody.AddForce(direction.normalized * (GravitationalPull / distance) * PlayerRigidbody.mass * Time.fixedDeltaTime);
    }
}
