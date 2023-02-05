using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class enemieHelper : MonoBehaviour
{
    public UnityEvent<Vector2> OnMove;
    [SerializeField] private GameObject player;
    private Vector2 direction;

    public bool canMove = true;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (player.GetComponent<playerController>().IsDead) return;

        if (canMove) 
        {
            direction = (player.transform.position - transform.position);
        }
        
        if (!canMove) 
        {
            direction = Vector2.zero;
            StartCoroutine(delayToCanMove());
        }

        OnMove?.Invoke(direction.normalized);
    }

    IEnumerator delayToCanMove() 
    {
        yield return new WaitForSeconds(0.1f);
        canMove = true;
    }
}
