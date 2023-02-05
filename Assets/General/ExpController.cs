using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class ExpController : MonoBehaviour
{
    private Slider expBar;
    private Text expText;
    public int levelReferance { get; private set; }

    private bool OnceAwake = true;
    [SerializeField] private weaponControl WeaponControl;
    [SerializeField] private playerController player;
    [SerializeField] private Light2D LightOfPlayer;

    public float timeBtwSpawns, startTimeSpawns;
    public GameObject echo;

    private void Awake()
    {
        levelReferance = 0;
        expBar = GameObject.Find("ExpBar").GetComponent<Slider>();
        expText = GameObject.Find("Level").GetComponent<Text>();
    }

    private void Update()
    {
        foreach (Collider2D collinder in Physics2D.OverlapBoxAll(transform.position, new Vector3(0.5f, 1, 1), 0)) 
        {
            if (collinder.tag == "Experiance") 
            {
                expBar.value++;
                Destroy(collinder.gameObject);
            }
        }

        // Display Player level
        expText.text = $"Lv. {levelReferance.ToString()}";

        // Leveling function
        if (expBar.value == expBar.maxValue) 
        {
            expBar.value = 0;
            expBar.maxValue += 2;
            WeaponControl.timeDelay -= 0.25f;
            WeaponControl.speedRotate += 20f;
            player.speedForce += 10;
            LightOfPlayer.pointLightOuterRadius += 0.2f;
            levelReferance++;
        }

        // Check Evolve
        if (levelReferance == 8 && OnceAwake) 
        {
            OnceAwake = false;
            WeaponControl.timeDelay = 1.5f;
            player.speedForce += 60;
        }

        // Create speed illusion
        if (!OnceAwake) 
        {
            if (player.movementInput != Vector2.zero)
            {
                if (player.movementInput.x < 0)
                {
                    echo.GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (player.movementInput.x > 0)
                {
                    echo.GetComponent<SpriteRenderer>().flipX = false;
                }

                if (timeBtwSpawns <= 0)
                {
                    GameObject instance = Instantiate(echo, transform.position, Quaternion.identity);
                    Destroy(instance, 0.7f);
                    timeBtwSpawns = startTimeSpawns;
                }
                else
                {
                    timeBtwSpawns -= Time.deltaTime;
                }
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.5f, 1, 1));
    }
}
