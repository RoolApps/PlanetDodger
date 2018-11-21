using UnityEngine;
using System.Collections;

public class PlanetRotation : MonoBehaviour
{
    private float RotationSpeed;
    private float Angle = 0;

    private void Awake()
    {
        RotationSpeed = Random.Range(-1f, 1f);
    }

    private void LateUpdate()
    {
        Angle += RotationSpeed;
        transform.localRotation = Quaternion.Euler(0, 0, Angle);
    }
}
