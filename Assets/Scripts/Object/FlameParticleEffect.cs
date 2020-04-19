using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameParticleEffect : MonoBehaviour
{
    private float lifeTime = 1.0f;

    void Start()
    {
        Invoke("destroyParticles", lifeTime);
    }

    void destroyParticles()
    {
        Destroy(gameObject);
    }
}
