using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class TankAltFire : MonoBehaviour
{
    public TextMeshProUGUI J_AmmoCounterText;
    public Rigidbody J_Shell2;
    public Transform m_FireTransform;
    public float m_LaunchForce = 50f;
    public int J_Ammo = 0; 

    private void Update()
    {
        int Ammo = J_Ammo;
        J_AmmoCounterText.text = string.Format("Ammo: {00}" , Ammo);
        if (Input.GetButtonUp("Fire2") && J_Ammo > 0)
        {
            
            Fire2();
        }
    }

    private void Fire2()
    {
        J_Ammo = J_Ammo - 1;
        Rigidbody shellInstance = Instantiate(J_Shell2, m_FireTransform.position, m_FireTransform.rotation);
        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;
        
    }

    public void AddAmmo(int newAmmo)
    {
        J_Ammo += newAmmo;
    }

}