using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class enemieController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator ami;
    private SpriteRenderer spi;
    [SerializeField] private GameObject experiance;

    private Vector2 movementInput;
    [SerializeField] private float moveSpeed = 130, enemieDamage = 1;
    private bool IsDead = false, readyAttack = true;
    public Health health;
    [SerializeField] private float recSize;
    private GameObject deathCount;

    private void Awake()
    {
        deathCount = GameObject.Find("CountDeath");
        spi = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        ami = GetComponent<Animator>();
        health = GetComponent<Health>();
        health.InitializeHealth(3);
    }

    private void Update()
    {
        // Check if dead
        if (IsDead) 
        {
            rb.velocity = Vector2.zero;
            return;
        }

        foreach (Collider2D collinder in Physics2D.OverlapBoxAll(transform.position, new Vector3(0.7f, 1, 1), 0)) 
        {
            if (collinder.tag == "Player" && readyAttack && !IsDead) 
            {
                Health health;
                if (health = collinder.GetComponent<Health>()) 
                {
                    health.OnAttack(gameObject, enemieDamage);
                    StartCoroutine(delayToAttack());
                }
            }
        }

        rb.velocity = movementInput * moveSpeed * Time.fixedDeltaTime;
        if (movementInput != Vector2.zero) 
        {
            if (movementInput.x < 0) spi.flipX = true;
            else if (movementInput.x > 0) spi.flipX = false;
        }
    }

    public void OnMove(Vector2 value) 
    {
        movementInput = value;
    }

    public void OnHits() 
    {
        ami.SetTrigger("IsHits");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.7f,1,1));
    }

    IEnumerator delayToAttack()
    {
        readyAttack = false;
        yield return new WaitForSeconds(0.5f);
        readyAttack = true;
    }

    public void OnDeath(GameObject obj) 
    {
        IsDead = true;
        ami.SetBool("IsDead", true);
        StartCoroutine(delayToDelete());
    }

    IEnumerator delayToDelete() 
    {
        yield return new WaitForSeconds(1f);
        deathCount.GetComponent<CounterDeath>().enemieDeath++;
        Destroy(gameObject);
        Instantiate(experiance, transform.position, transform.rotation);
    }

}
