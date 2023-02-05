using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class bulletHitbox : MonoBehaviour
{
    [SerializeField] private float radius, damageBullet;
    private Animator ami;
    private Rigidbody2D rb;
    private bool alreadyAttack = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ami = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemies" || collision.tag == "Wall" && alreadyAttack) 
        {
            foreach (Collider2D collinder in Physics2D.OverlapCircleAll(transform.position, radius))
            {
                Health health;
                if (health = collinder.GetComponent<Health>())
                {
                    health.OnAttack(gameObject, damageBullet);
                }
                ami.SetTrigger("IsHits");
                rb.velocity = Vector2.zero;
                StartCoroutine(delayToDeletObject());
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator delayToDeletObject() 
    {
        alreadyAttack = false;
        yield return new WaitForSeconds(0.27f);
        Destroy(gameObject);
    }
}
