using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour {

    public float Acceleration;
    public ParticleSystem System;
    public Rigidbody2D Rigidbody;
    public Transform Player;
    public Camera Camera;
    public bool ScriptEnabled;
    public float JoystickRadius;
    
    private bool mousePressed = false;
    private Vector3 center;

    // Update is called once per frame
    private void Awake()
    {
        Acceleration = GameSettings.Ship.Acceleration;
    }

    private void FixedUpdate ()
    {
        if (ScriptEnabled && mousePressed)
        {
            var direction = Input.mousePosition - center;

            transform.localPosition = direction.normalized * Mathf.Clamp(direction.magnitude / 100, 0, JoystickRadius);

            float AngleRad = Mathf.Atan2(direction.y, direction.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            Player.rotation = Quaternion.Euler(0, 0, AngleDeg);

            Rigidbody.AddForce(direction.normalized * Acceleration * Time.fixedDeltaTime * transform.localPosition.magnitude / JoystickRadius);

            if (System.isStopped)
            {
                System.Play();
            }
        }
        else if (System.isPlaying)
        {
            System.Stop();
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
