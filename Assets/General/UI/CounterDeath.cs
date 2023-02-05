using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterDeath : MonoBehaviour
{
    [SerializeField] private Text displayDeathCount;
    public int enemieDeath;

    private void Awake()
    {
        enemieDeath = 0;
    }

    private void Update()
    {
        displayDeathCount.text = $"{enemieDeath.ToString()}";
    }
}
