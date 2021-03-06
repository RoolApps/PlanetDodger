﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public Transform Camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        var xOffset = (Camera.transform.position.x % (transform.lossyScale.x * 2)) / (transform.lossyScale.x * 2);
        var yOffset = (Camera.transform.position.y % (transform.lossyScale.y * 2)) / (transform.lossyScale.y * 2);

        mat.mainTextureOffset = new Vector2(xOffset, yOffset);
    }
}
