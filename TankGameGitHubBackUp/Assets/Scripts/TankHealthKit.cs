using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealthKit : MonoBehaviour
{
    public TankHealth tankHealth;
    public GameManager gameManager;
    public int J_AddedHealth = 100;

    void Update()
    {
        gameObject.transform.Rotate(0, Time.deltaTime * 60, 0);
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tankHealth.AddHealth(J_AddedHealth);
            Destroy(gameObject);
        }
    }
}
