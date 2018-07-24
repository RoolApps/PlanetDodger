using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloController : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
        Behaviour halo = GetComponent("Halo") as Behaviour;
        foreach(var property in halo.GetType().GetProperties())
        {
            try
            {
                Debug.Log("Property name: " + property.Name + ", Property value: " + property.GetValue(halo, null).ToString());
            }
            catch(System.Exception)
            {

            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
