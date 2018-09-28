using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipLoader : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        GetComponent<SpriteRenderer>().sprite = GameSettings.Ship.Sprite;
	}
}
