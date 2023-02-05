using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float healthMax, healthCurrent;
    public bool IsDead;

    public UnityEvent<GameObject> OnHits, OnDeath;

    public void InitializeHealth(float health) 
    {
        healthMax = health;
        healthCurrent = health;
        IsDead = false;
    }

    public void OnAttack(GameObject sender, float damage) 
    {
        if (gameObject == sender || IsDead) return;

        if (healthCurrent > 0) 
        {
            healthCurrent -= damage;
            if (gameObject.tag == "Enemies") 
            {
                OnHits?.Invoke(gameObject);
            }
        }
        if (healthCurrent <= 0) 
        {
            IsDead = true;
            OnDeath?.Invoke(gameObject);
        }
    }


}
