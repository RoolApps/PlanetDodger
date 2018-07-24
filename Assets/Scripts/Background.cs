using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        var xOffset = (transform.position.x % (transform.lossyScale.x * 2)) / (transform.lossyScale.x * 2);
        var yOffset = (transform.position.y % (transform.lossyScale.y * 2)) / (transform.lossyScale.y * 2);

        mat.mainTextureOffset = new Vector2(xOffset, yOffset);
    }
}
