using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RockThrow;

/// <summary>
/// Particle controller.
/// Designed for object pooling and applying to hit objects
/// </summary>
namespace RockThrow
{
    public class ParticleController : MonoBehaviour
    {
        private ParticleSystem[] particles = new ParticleSystem[3];

        // Use this for initialization
        private void Start()
        {
            particles = GetComponentsInChildren<ParticleSystem>();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void FireParticles(Vector2 firePos)
        {
            transform.position = new Vector3(firePos.x, firePos.y, 50.0f);
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].Play();
            }
        }
    }
}