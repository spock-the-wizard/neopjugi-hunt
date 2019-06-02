using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDecalTool : MonoBehaviour
{
    public float decalSizeMin = 0.5f;
    public float decalSizeMax = 1.5f;
    private int particleDecalDataIndex;
    public int maxDecals = 10;
    private ParticleDecalData[] particleData;
    private ParticleSystem.Particle[] particles;
    private ParticleSystem decalParticleSystem;
    // Start is calleabefore the first frame update
    void Start()
    {
        decalParticleSystem = GetComponent<ParticleSystem>();
        particleData = new ParticleDecalData[maxDecals];
        particles = new ParticleSystem.Particle[maxDecals];
        for (int i = 0; i < maxDecals; i++)
        {
            particleData[i] = new ParticleDecalData();
        }
    }

    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        SetParticleData(particleCollisionEvent, colorGradient);
        DisplayParticles();
    }
    void DisplayParticles()
    {
        for (int i = 0; i < particleData.Length; i++)
        {
            particles[i].position = particleData[i].position;
            particles[i].rotation3D = particleData[i].rotation;
            particles[i].startSize = particleData[i].size;
            particles[i].startColor = particleData[i].color;
        }

        decalParticleSystem.SetParticles(particles, particles.Length);
    }
    void SetParticleData(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        if(particleDecalDataIndex >= maxDecals)
        {
            particleDecalDataIndex = 0;
        }

        particleData[particleDecalDataIndex].position = particleCollisionEvent.intersection;
        Vector3 particleRotationEuler = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        particleRotationEuler.z = Random.Range(0, 360) ;
        particleData[particleDecalDataIndex].rotation = particleRotationEuler;
        particleData[particleDecalDataIndex].size = Random.Range(decalSizeMin, decalSizeMax);
        particleData[particleDecalDataIndex].color = colorGradient.Evaluate(Random.Range(0f,1f));

        particleDecalDataIndex++;
    }
}
