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

        var xOffset = (transform.position.x % transform.lossyScale.x) / transform.lossyScale.x;
        var yOffset = (transform.position.y % transform.lossyScale.y) / transform.lossyScale.y;

        mat.mainTextureOffset = new Vector2(xOffset, yOffset);
    }
}
