using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float acceleration;
    //private float maxSpeed = 10;
    //private Vector3 speed = new Vector2(0, 0);
    private Vector3 screenCenter;

	// Use this for initialization
	void Start () {
        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);
	}
	
	// Update is called once per frame
	void Update() {
        if (Input.GetMouseButton(0))
        {
            var direction = Input.mousePosition - screenCenter;
            float AngleRad = Mathf.Atan2(direction.y, direction.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(direction.normalized * acceleration * Time.fixedDeltaTime);
        }
    }
}
