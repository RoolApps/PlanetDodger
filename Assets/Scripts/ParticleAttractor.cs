using System.Collections;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class ParticleAttractor: MonoBehaviour
{
    public Transform Target;
    public float Speed = 5f;

    private ParticleSystem system;
    private ParticleSystem.Particle[] particles;
    private int numParticlesAlive;
    void Start()
    {
        system = GetComponent<ParticleSystem>();
    }
    void Update()
    {

        particles = new ParticleSystem.Particle[system.main.maxParticles];
        numParticlesAlive = system.GetParticles(particles);
        float step = Speed * Time.deltaTime;
        for (int i = 0; i < numParticlesAlive; i++)
        {
            particles[i].position = Vector3.LerpUnclamped(particles[i].position, Target.position, step);
        }
        system.SetParticles(particles, numParticlesAlive);
    }
}