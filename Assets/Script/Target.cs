using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float Health = 50f;
    public void TakeDamage(float Amount)
    {
        Health -= Amount;
        if(Health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);    
    }
}
