using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float m_MaxLifeTime = 5f;
    public float m_MaxDamage = 34;
    public float m_ExplosionRadius = 5;
    public float m_ExplosionForce = 75f;

    public ParticleSystem m_ExplosionParticles;
    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();

        if (targetRigidbody != null)
        {
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

            if (targetHealth != null)
            {
                float damage = CalculateDamage(targetRigidbody.position);
                targetHealth.TakeDamage(damage);
            }
        }

        m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        Destroy(gameObject);
    }
    
    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        float damage = relativeDistance * m_MaxDamage;
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}
