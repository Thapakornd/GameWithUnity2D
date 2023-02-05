using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    /*    ----> Spawn position <----
     *    (-11,-3) ,  (-11, 3)           :  LEFT
     *    (-6, 9)  ,  (0, 9)  ,  (6, 9)  :  TOP
     *    (13, 4)  ,  (13 ,-1)           :  RIGHT
     *    (-7, -6) ,  (0, -6) ,  (6, -9) :  BOTTOM
     */

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemiePrefab;
    public float delaySpawn = 1, delaySetSpawn = 1;
    
    private Vector2[] positionSpawn = {new Vector2(-11, 3), new Vector2(-11, 3)
                        , new Vector2(-6, 9), new Vector2(0, 9), new Vector2(6, 9)
                        , new Vector2(13, 4), new Vector2(13, -1)
                        , new Vector2(-7, -6), new Vector2(0, -6), new Vector2(6, -9)};

    private void Update()
    {
        if (player == null) return;
        
        if (delaySpawn >= delaySetSpawn) 
        {
            int spawnPos = Random.Range(0, positionSpawn.Length);
            Instantiate(enemiePrefab, (Vector3)positionSpawn[spawnPos] + player.transform.position, transform.rotation);
            delaySpawn = 0;
        }

        // Add time to delay spawner
        if (delaySpawn < delaySetSpawn) delaySpawn += Time.deltaTime;
    }
}
