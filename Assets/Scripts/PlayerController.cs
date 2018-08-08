using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float acceleration;
    public ParticleSystem system;
    public bool ScriptEnabled;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update() {
        if (ScriptEnabled && Input.GetMouseButton(0))
        {
            var screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
            var direction = Input.mousePosition - screenCenter;
            float AngleRad = Mathf.Atan2(direction.y, direction.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(direction.normalized * acceleration * Time.fixedDeltaTime);

            if(system.isStopped)
            {
                system.Play();
            }
        }
        else if(system.isPlaying)
        {
            system.Stop();
        }
    }
}
