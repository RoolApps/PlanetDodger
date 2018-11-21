using UnityEngine;
using System.Linq;
using System.Collections;

public class Planet : MonoBehaviour
{
    private float pullRadius = 0f;
    private float radius = 0f;
    

    public ParticleSystem ShipParticleSystem { get; set; }
    public SpriteRenderer CircleSpriteRenderer { get; private set; }
    public Vector3 Center
    {
        get
        {
            return CircleSpriteRenderer.bounds.center;
        }
    }

    public void Awake()
    {
        CircleSpriteRenderer = GetComponentsInChildren<SpriteRenderer>().Single(renderer => renderer.gameObject.name == "GravityCircle");
    }

    public float Radius
    {
        get
        {
            return radius;
        }
        set
        {
            radius = value;
            pullRadius = value * 4 * 4;
        }
    }

    public float PullRadius
    {
        get
        {
            return pullRadius;
        }
    }
}
