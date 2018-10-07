using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipLoader : MonoBehaviour {

    public JoystickController controller;

	// Use this for initialization
	void Awake () {
        Debug.Log(GameSettings.Current.SelectedShip);
        var prefab = Ship.GetShip(GameSettings.Current.SelectedShip).Prefab;
        
        var spaceship = Instantiate(prefab, new Vector3(0, 0, 10), Quaternion.identity) as GameObject;
        GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = spaceship.transform;
        spaceship.name = "Player";
        controller.SetShip(spaceship.transform);
    }
}
