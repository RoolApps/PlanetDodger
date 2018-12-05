using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float PullMultiplier;
    public float DistanceMultiplier;
    
    private Rigidbody2D PlayerRigidbody;
    private SpriteRenderer PlayerSpriteRenderer;
    private float GravityMultiplier = 1f;

    private void Awake()
    {
        PlayerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        PlayerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        GameSession.Current.SpaceshipCrashed += Current_SpaceshipCrashed;
        GameSession.Current.GravityChanged += Current_GravityChanged;
    }

    private void Current_SpaceshipCrashed(object sender, System.EventArgs e)
    {
        GameSession.Current.SpaceshipCrashed -= Current_SpaceshipCrashed;
        GameSession.Current.GravityChanged -= Current_GravityChanged;
    }

    private void Current_GravityChanged(object sender, GameSession.GravityEventArgs e)
    {
        this.GravityMultiplier = e.Gravity;
    }

    private void FixedUpdate()
    {
        var planets = Physics2D.OverlapCircleAll(PlayerSpriteRenderer.bounds.center, 20).Select(collider => collider.gameObject).Where(gameObject => gameObject.name == "Planet(Clone)");
        foreach (var planetGameObject in planets)
        {
            var planet = planetGameObject.GetComponent<Planet>();
            var shipParticleSystem = planet.ShipParticleSystem;

            Vector3 direction = planet.Center - PlayerSpriteRenderer.bounds.center;
            planet.CircleSpriteRenderer.color = new Color(1, 1, 1, Mathf.Clamp(1f - (direction.magnitude - planet.PullRadius * 0.5f) / (planet.PullRadius * 0.5f), 0f, 1f));

            if (direction.magnitude > planet.PullRadius)
            {
                if (shipParticleSystem.isPlaying)
                {
                    shipParticleSystem.Stop();
                }
            }
            else
            {
                if (!shipParticleSystem.isPlaying)
                {
                    shipParticleSystem.Play();
                }
                shipParticleSystem.transform.right = direction;

                var main = shipParticleSystem.main;
                main.startSpeedMultiplier = Mathf.Sqrt(direction.magnitude);

                float distance = direction.sqrMagnitude * DistanceMultiplier + 1;

                var gravitationalPull = planet.Radius * planet.Radius * PullMultiplier * GravityMultiplier;
                
                PlayerRigidbody.AddForce(direction.normalized * (gravitationalPull / distance) * PlayerRigidbody.mass * Time.deltaTime);
            }
        }
    }
}
