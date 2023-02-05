using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWaponScript : MonoBehaviour
{
    [SerializeField] private float damageMelee = 0.5f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemies") 
        {
            Health health;
            enemieHelper enemieHelp;
            if ((health = collision.GetComponent<Health>()) && (enemieHelp = collision.GetComponent<enemieHelper>()))
            {
                health.OnAttack(gameObject, damageMelee);
                enemieHelp.canMove = false;
            }
        }
    }
}
