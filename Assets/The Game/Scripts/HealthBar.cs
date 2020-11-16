using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public playerHealth playerHealth;
    void Update()
    {
        float healthProzent;
        if (playerHealth.getHealthProzent() <= 0)
        {
            healthProzent = 0;
        }
        else {
            healthProzent = playerHealth.getHealthProzent();
        }
        transform.localScale = new Vector3(healthProzent, 1);
    }
}
