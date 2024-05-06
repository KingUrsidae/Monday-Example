using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;
    public GameObject m_ExplosionPrefab;
    public float J_FireFreashHold = 75f;
    public float J_FireFreashHold2 = 50f;
    public float J_FireFreashHold3 = 25f;
    public GameObject J_Fire;
    public GameObject J_Fire2;
    public GameObject J_Fire3;

    public float m_CurrentHealth; 
    private bool m_Dead;
    private ParticleSystem m_ExplosionParticles; 

    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }
    
    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
        J_Fire.gameObject.SetActive(false);
        J_Fire2.gameObject.SetActive(false);
        J_Fire3.gameObject.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        m_CurrentHealth -= amount;
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath(); 
        }
        if (m_CurrentHealth <= J_FireFreashHold)
        {
            J_Fire.gameObject.SetActive(true);
        }
        if (m_CurrentHealth <= J_FireFreashHold2)
        {
            J_Fire2.gameObject.SetActive(true);
        }
        if (m_CurrentHealth <= J_FireFreashHold3)
        {
            J_Fire3.gameObject.SetActive(true);
        }
    }
    private void OnDeath()
    {
        m_Dead = true;

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);
        m_ExplosionParticles.Play();

        gameObject.SetActive(false); 
    }

    public void AddHealth(int newHealth)
    {
        m_CurrentHealth += newHealth;
        if (m_CurrentHealth > m_StartingHealth)
        {
            m_CurrentHealth = m_StartingHealth;
        }

        if (m_CurrentHealth >= J_FireFreashHold)
        {
            J_Fire.gameObject.SetActive(false);
        }

        if (m_CurrentHealth >= J_FireFreashHold2)
        {
            J_Fire2.gameObject.SetActive(false);
        }

        if (m_CurrentHealth >= J_FireFreashHold3)
        {
            J_Fire3.gameObject.SetActive(false);
        }
    }

   
}

