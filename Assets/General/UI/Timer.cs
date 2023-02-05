using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text displayTimer;
    [SerializeField] private GameObject timeSpawn;
    private float timeCounter = 0;

    private void Update()
    {
        // Find min and sec value
        float minTime = Mathf.FloorToInt(timeCounter / 60);
        float secTime = Mathf.FloorToInt(timeCounter % 60);

        // Set spawn enemies time each current timer
        timeSpawn.GetComponent<SpawnEnemies>().delaySetSpawn = 3 - (minTime * 0.4f);

        // Show to display
        displayTimer.text = $"{((int)minTime / 10).ToString()}{(minTime % 10).ToString()}:{((int)secTime / 10).ToString()}{(secTime % 10).ToString()}";

        // Count time +1 sec
        timeCounter += Time.deltaTime;
    }
}
