using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private static ParticleManager instance;
    void Start()
    {
        instance = GetComponent<ParticleManager>();
    }

    public static ParticleManager Instance()
    {
        return instance;

    }

    public void Get(string name, Vector3 spawnTransform, int maxParticle, float destroyTime = 0)
    {
        GameObject particle = Pool.Instance().Get(name);
        if (spawnTransform != Vector3.zero)
        {
            particle.transform.position = spawnTransform;
        }
        particle.GetComponent<ParticleSystem>().maxParticles = maxParticle;
        particle.GetComponent<ParticleSystem>().Play();
        StartCoroutine(SendBackParticle(particle, destroyTime));



    }
    IEnumerator SendBackParticle(GameObject particle, float time)
    {

        yield return new WaitForSeconds(time);
        Pool.Instance().SendBack(particle);
        yield break;
    }
}
