using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JoystickController : MonoBehaviour {

    public float Acceleration;
    public Camera Camera;
    public float JoystickRadius;

    private Rigidbody2D playerRigidbody;
    private ParticleSystem[] systems;
    private Transform player;

    private bool scriptEnabled = false;
    private bool mousePressed = false;
    private Vector3 center;
    private const float maxSpeed = 7;

    // Update is called once per frame
    private void Awake()
    {
        Acceleration = Ship.GetShip(GameSettings.Current.SelectedShip).Acceleration;
        GameSession.Current.SpaceshipCrashed += Current_SpaceshipCrashed;
    }

    private void Current_SpaceshipCrashed(object sender, System.EventArgs e)
    {
        GameSession.Current.SpaceshipCrashed -= Current_SpaceshipCrashed;
        scriptEnabled = false;
    }

    private IEnumerable<Transform> GetTransforms(Transform transform)
    {
        foreach(Transform childTransform in transform)
        {
            yield return childTransform;
        }
    }

    public void SetShip(Transform spaceship)
    {
        player = spaceship;
        var particleSystems = player.Find("ParticleSystems");
        systems = GetTransforms(particleSystems.transform).Select(transform => transform.gameObject.GetComponent<ParticleSystem>()).ToArray();
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        scriptEnabled = true;
    }

    private void FixedUpdate ()
    {
        if (player == null)
        {
            return;
        }
        if (scriptEnabled && mousePressed)
        {
            var direction = Input.mousePosition - center;

            transform.localPosition = direction.normalized * Mathf.Clamp(direction.magnitude / 100, 0, JoystickRadius);

            player.transform.right = direction;

            var force = direction.normalized * Acceleration * Time.fixedDeltaTime * transform.localPosition.magnitude / JoystickRadius;
            var speed = playerRigidbody.velocity;
            force.x = Mathf.Clamp(force.x, -maxSpeed - Mathf.Clamp(speed.x, -maxSpeed, 0), maxSpeed - Mathf.Clamp(speed.x, 0, maxSpeed));
            force.y = Mathf.Clamp(force.y, -maxSpeed - Mathf.Clamp(speed.y, -maxSpeed, 0), maxSpeed - Mathf.Clamp(speed.y, 0, maxSpeed));
            playerRigidbody.AddForce(force);
            foreach (var system in systems)
            {
                if (system.isStopped)
                {
                    system.Play();
                }
            }
        }
        else
        {
            foreach (var system in systems)
            {
                if (system.isPlaying)
                {
                    system.Stop();
                }
            }
        }
    }

    private void OnMouseDown()
    {
        center = Input.mousePosition;
        mousePressed = true;
    }

    private void OnMouseUp()
    {
        mousePressed = false;
        transform.localPosition = new Vector3();
    }
}
