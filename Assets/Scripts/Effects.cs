using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects
{
    private static Effects current;
    EZCameraShake.CameraShaker shaker;
    Object explosionPrefab = Resources.Load("Prefabs/Explosion", typeof(GameObject));

    private Effects()
    {
        shaker = GameObject.Find("CinemachineCamera").GetComponent<EZCameraShake.CameraShaker>();
    }

    public static Effects Current
    {
        get
        {
            return current ?? (current = new Effects());
        }
    }

    public void Forget()
    {
        shaker = null;
        current = null;
    }

	public void ShakeCamera()
    {
        shaker.ShakeOnce(20, 1, 0, 2);
    }

    public void Explode(Vector3 position)
    {
        GameObject clone = Object.Instantiate(explosionPrefab, position, Quaternion.identity) as GameObject;
        Object.Destroy(clone, 2);
    }
}
