using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyTankShooting : MonoBehaviour
{
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public float m_lanchForce = 30f;
    public float m_ShootDeley = 1f;

    private bool m_CanShoot;
    private float m_ShootTimer;
    private void Awake()
    {
        m_CanShoot = false;
        m_ShootTimer = 0;
    }

    
    private void Update()
    {
        if (m_CanShoot == true)
        {
            m_ShootTimer -= Time.deltaTime;
            if (m_ShootTimer <= 0)
            {
                m_ShootTimer = m_ShootDeley;
                Fire();
            }
        }
    }

    private void Fire()
    {
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        shellInstance.velocity = m_lanchForce * m_FireTransform.forward; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_CanShoot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_CanShoot = false;
        }
    }
}
