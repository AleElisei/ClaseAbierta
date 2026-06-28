using System.Collections;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float time = 10;
    [SerializeField] GameObject[] enemyPrefabs;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true) 
        {
            yield return new WaitForSeconds(time);
            GameObject newEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
            newEnemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        }
    }
}
