using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpObject : MonoBehaviour
{
    [SerializeField] private float radius, speedMoveItem;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        foreach (Collider2D collinder in Physics2D.OverlapCircleAll(transform.position, radius)) 
        {
            if (collinder.tag == "Player") 
            {
                // Move into player
                Vector2 direction = collinder.transform.position - transform.position;
                rb.velocity = direction.normalized * speedMoveItem;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
