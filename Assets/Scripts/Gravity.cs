using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    // Use this for initialization
    
    public float PullMultiplier;
    public float DistanceMultiplier; // Factor by which the distance affects force

    private float GravitationalPull; // Pull force
    private float PullRadius; // Radius to pull
    private Rigidbody2D PlayerRigidbody;
    private SpriteRenderer PlayerSpriteRenderer;
    private SpriteRenderer PlanetSpriteRenderer;
    private ParticleSystem PlanetParticleSystem;

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

    private ParticleSystem GetParticleSystem()
    {
        if(PlanetParticleSystem == null)
        {
            var particleSystem = GetComponentInChildren<ParticleSystem>();
            PlanetParticleSystem = particleSystem;
        }
        return PlanetParticleSystem;
    }

    // Function that runs on every physics frame
    void FixedUpdate()
    {
        Vector3 direction = PlanetSpriteRenderer.bounds.center - PlayerSpriteRenderer.bounds.center;
        
        var particleSystem = GetParticleSystem();
        if(particleSystem != null)
        {
            float AngleRad = Mathf.Atan2(direction.y, direction.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            particleSystem.transform.rotation = Quaternion.Euler(180 - AngleDeg, 90, -90);

            var main = particleSystem.main;
            main.startSpeedMultiplier = Mathf.Sqrt(direction.magnitude);
            
        }

        if (direction.magnitude > PullRadius)
        {
            if (PlanetParticleSystem.isPlaying)
            {
                PlanetParticleSystem.Stop();
            }
            return;
        }
        else
        {
            if (!PlanetParticleSystem.isPlaying)
            {
                PlanetParticleSystem.Play();
            }
        }

        float distance = direction.sqrMagnitude * DistanceMultiplier + 1;

        PlayerRigidbody.AddForce(direction.normalized * (GravitationalPull / distance) * PlayerRigidbody.mass * Time.fixedDeltaTime);
        
    }
}
