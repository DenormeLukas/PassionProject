using UnityEngine;
using System.Collections.Generic;

public class ExplosionTest : MonoBehaviour
{
    public ParticleSystem deathParticles;

    void Start()
    {
      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Die();
        }
    }

    public void Die()
    {
        deathParticles.transform.position = transform.position;
        deathParticles.Play();
    }
}
