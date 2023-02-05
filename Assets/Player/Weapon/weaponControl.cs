using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponControl : MonoBehaviour
{
    public Vector2 movementCurrent { get; set; }
    private Vector2 movementInused;
    private Vector2[] bulletMove;

    [SerializeField] private Transform aimPoint, weaponLeft;
    [SerializeField] private GameObject bullet;
    [SerializeField] float bulletForce;
    private ExpController expRef;

    public float timeAttack = 3, timeDelay = 3, speedRotate = 20;
    private float positionRotate;

    private Vector3 positionSet, rotationSet;

    private void Awake()
    {
        expRef = GetComponentInParent<ExpController>();
    }

    private void Update()
    {
        if (movementCurrent != Vector2.zero) 
        {
            if (movementCurrent.x != 0) movementInused.x = movementCurrent.x;
        }

        if (timeAttack >= timeDelay && movementInused.x != 0)
        {
            OnFireLeft(movementInused);
            timeAttack = 0;
        }
        if (timeAttack < timeDelay) timeAttack += Time.deltaTime;

        // Run attack to right weapon
        OnFireRight();
    }

    private void OnFireLeft(Vector2 movement)
    {
        // Set position reference with "Weapon" Object
        Vector2[] movementBulletRight = { new Vector2(1f, 1f), new Vector2(1f, 0), new Vector2(1f, -1f) };
        Vector2[] movementBulletLeft = { new Vector2(-1f, -1f), new Vector2(-1f, 0), new Vector2(-1f, 1f)};

        if (movement.x > 0) 
        {
            positionSet = new Vector3(1.5f, -0.15f);
            rotationSet = new Vector3(0, 0, 90);
            bulletMove = movementBulletRight;
        }else if (movement.x < 0) 
        {
            positionSet = new Vector3(-1.5f, -0.15f);
            rotationSet = new Vector3(0, 0, -90);
            bulletMove = movementBulletLeft;
        }

        // Check if level reach Lv. 10;
        if (expRef.levelReferance < 10) 
        {
            movement.y = 0;
            GameObject bulletPrefab = Instantiate(bullet, transform.position + positionSet, Quaternion.Euler(rotationSet));
            Rigidbody2D rb = bulletPrefab.GetComponent<Rigidbody2D>();
            rb.AddForce(movement * bulletForce, ForceMode2D.Impulse);
        }
        else if (expRef.levelReferance == 8) 
        {
            for (int i=0; i<3; i++) 
            {
                GameObject bulletPrefab = Instantiate(bullet, transform.position + positionSet, Quaternion.Euler(rotationSet));
                Rigidbody2D rb = bulletPrefab.GetComponent<Rigidbody2D>();
                rb.AddForce(bulletMove[i] * bulletForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnFireRight() 
    {
        // Get new Position rotate
        positionRotate -= (Time.deltaTime * speedRotate) % 360;
        weaponLeft.rotation = Quaternion.Lerp(weaponLeft.rotation, Quaternion.Euler(new Vector3(0, 0, positionRotate)), speedRotate);        
    }

    IEnumerator deleyAttack() 
    {
        yield return new WaitForSeconds(1f);
    }
}
