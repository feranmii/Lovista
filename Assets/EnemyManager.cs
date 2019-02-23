using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventCallbacks;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPositions;
    [Header("Wave Variables")]
    
    
    public List<Enemy> allEnemies;
    public int currentWave;
    public int totalWave;
    public int targetEnemyNumber;
    public int enemyCount;
    public bool waveFinished;
    
    
    private void Start()
    {
        enemyCount = allEnemies.Count;
        EventManager.Instance.RegisterListener<OnEnemyDie>(EnemyDeath);
        allEnemies = FindObjectsOfType<Enemy>().ToList();
        
    }

    private void Update()
    {
        if (enemyCount == 0)
        {
            if (targetEnemyNumber != 0)
            {
                SpawnEnemy();
            }
        }
    }

    void EnemyDeath(OnEnemyDie ed)
    {        
        if (allEnemies.Contains(ed.en))
        {
            allEnemies.Remove(ed.en);
        }
        
        enemyCount = allEnemies.Count;
        targetEnemyNumber--;
    }

    public void SpawnEnemy()
    {
        var numberToSpawn = Random.Range(0, 4);
        for (int i = 0; i < numberToSpawn; i++)
        {
            int posToSpawn = Random.Range(0, spawnPositions.Length);
            Instantiate(enemyPrefab, spawnPositions[posToSpawn].position, Quaternion.identity);
        }
    }
    
}
