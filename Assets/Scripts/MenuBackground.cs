using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackground : MonoBehaviour {

    private float xOffset = 0f;
    private float yOffset = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        xOffset += 0.0002f;
        yOffset += 0.0001f;

        mat.mainTextureOffset = new Vector2(xOffset, yOffset);
    }
}
