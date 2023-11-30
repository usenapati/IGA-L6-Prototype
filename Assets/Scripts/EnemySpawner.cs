using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    public Transform[] spawnPoints;
    
    [SerializeField] private float spawnDelay;
    [SerializeField] private int activeEnemyCount = 0;
    [SerializeField] private int waveNumber = 0;
    [SerializeField] private bool isActive = true;

    public void DecrementEnemyCount()
    {
        if (activeEnemyCount > 0)
        {
            activeEnemyCount--;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (isActive)
        {
            if (activeEnemyCount == 0)
            {
                waveNumber++;
                // Spawn X enemies in random positions
                int enemiesToSpawn = Random.Range(1, spawnPoints.Length);
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    var random = Random.Range(0, spawnPoints.Length);
                    if (random > 0 && random < spawnPoints.Length)
                    {
                        Instantiate(enemyPrefab, spawnPoints[random].position,
                            enemyPrefab.transform.rotation);
                        activeEnemyCount++;
                    }

                }
            }
            yield return new WaitForSeconds(spawnDelay);

            yield return SpawnEnemyRoutine();
        }
        
    }
}
