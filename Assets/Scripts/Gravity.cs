using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    // Use this for initialization

    public float PullMultiplier;
    public float DistanceMultiplier; // Factor by which the distance affects force
    public ParticleSystem ShipParticleSystem;

    private float GravitationalPull; // Pull force
    private float PullRadius; // Radius to pull
    private float RotationSpeed;
    private float Angle = 0;
    private Rigidbody2D PlayerRigidbody;
    private SpriteRenderer PlayerSpriteRenderer;
    private SpriteRenderer PlanetSpriteRenderer;
    private SpriteRenderer PlanetCircleSpriteRenderer;

    private bool isInitialized = false;

    private void Start()
    {

    }

    private bool Initialize()
    {
        var player = GameObject.Find("Player");
        if (player != null)
        {
            PlayerRigidbody = player.GetComponent<Rigidbody2D>();
            PlayerSpriteRenderer = player.GetComponent<SpriteRenderer>();
            PlanetSpriteRenderer = GetComponent<SpriteRenderer>();
            PlanetCircleSpriteRenderer = GetComponentsInChildren<SpriteRenderer>().Single(renderer => renderer.gameObject.name == "GravityCircle");

            var radius = PlanetSpriteRenderer.bounds.size.x / 2;
            PullRadius = radius * 4;
            GravitationalPull = radius * radius * PullMultiplier;
            RotationSpeed = Random.Range(-1f, 1f);
            isInitialized = true;
        }
        return isInitialized;
    }

    // Function that runs on every physics frame
    void FixedUpdate()
    {
        if (!(isInitialized || Initialize()))
        {
            return;
        }
        Angle += RotationSpeed;
        transform.localRotation = Quaternion.Euler(0, 0, Angle);

        Vector3 direction = PlanetSpriteRenderer.bounds.center - PlayerSpriteRenderer.bounds.center;

        float AngleRad = Mathf.Atan2(direction.y, direction.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        ShipParticleSystem.transform.rotation = Quaternion.Euler(180 - AngleDeg, 90, -90);

        var main = ShipParticleSystem.main;
        main.startSpeedMultiplier = Mathf.Sqrt(direction.magnitude);

        PlanetCircleSpriteRenderer.color = new Color(1, 1, 1, Mathf.Clamp(1f - (direction.magnitude - PullRadius * 0.5f) / (PullRadius * 0.5f), 0f, 1f));

        if (direction.magnitude > PullRadius)
        {
            if (ShipParticleSystem.isPlaying)
            {
                ShipParticleSystem.Stop();
            }
            return;
        }
        else
        {
            if (!ShipParticleSystem.isPlaying)
            {
                ShipParticleSystem.Play();
            }
        }

        float distance = direction.sqrMagnitude * DistanceMultiplier + 1;

        PlayerRigidbody.AddForce(direction.normalized * (GravitationalPull / distance) * PlayerRigidbody.mass * Time.fixedDeltaTime);
    }
}
