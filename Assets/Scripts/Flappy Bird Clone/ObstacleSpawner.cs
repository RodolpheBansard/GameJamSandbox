using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform spawnPosition;
    public float spawnRate;

    public Vector2 randomPositionRange;

    public void LaunchSpawn() {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles(){
        while(true){
            GameObject obstacle = Instantiate(obstaclePrefab,spawnPosition.position,Quaternion.identity);
            obstacle.transform.position = new Vector2(spawnPosition.position.x,Random.Range(randomPositionRange.x,randomPositionRange.y));
            Destroy(obstacle,10f);
            yield return new WaitForSeconds(spawnRate);
        }        
    }

}
