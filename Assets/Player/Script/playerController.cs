using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public Vector2 movementInput;
    private Rigidbody2D rb;
    private SpriteRenderer spi;
    private Animator ami;

    public bool IsDead { get; private set; }
    [SerializeField] private GameObject weaponRotate;
    public float speedForce, rotateSpeed;
    [SerializeField] private Slider healthBar;
    public Health health;

    private void Awake()
    {
        IsDead = false;
        ami = GetComponent<Animator>();
        spi = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        health.InitializeHealth(10);
    }

    private void Update()
    {
        if (IsDead) 
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // Health Bar display
        healthBar.value = health.healthCurrent;

        rb.velocity = movementInput * speedForce * Time.fixedDeltaTime;
        if (movementInput != Vector2.zero) 
        {
            weaponRotate.GetComponent<weaponControl>().movementCurrent = movementInput;
            ami.SetBool("IsMoving", true);
            if (movementInput.x < 0) 
            {
                weaponRotate.transform.rotation = Quaternion.Lerp(weaponRotate.transform.rotation, Quaternion.Euler(0, 181f, 0), rotateSpeed * Time.fixedDeltaTime);
                spi.flipX = true;
            }else if (movementInput.x > 0) 
            {
                weaponRotate.transform.rotation = Quaternion.Lerp(weaponRotate.transform.rotation, Quaternion.Euler(0, -1f, 0), rotateSpeed * Time.fixedDeltaTime);
                spi.flipX = false;
            }
        }
        else 
        {
            ami.SetBool("IsMoving", false);
        }
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        movementInput = value.ReadValue<Vector2>();
    }

    public void OnDeath() 
    {
        IsDead = true;
        Destroy(GameObject.Find("Weapon"));
        ami.SetBool("IsDead", true);
    }
}
