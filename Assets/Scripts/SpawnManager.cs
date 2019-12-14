using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int EnemiesPerWave;
    public GameObject Enemy;
    public Transform[] SpawnPoints;
    public float TimeBetweenEnemies = 2f;
}


public class SpawnManager : MonoBehaviour
{
    public Wave[] Waves; // class to hold information per wave
    

    private int _totalEnemiesInCurrentWave;
    private int _enemiesInWaveLeft;
    private int _spawnedEnemies;

    private int _currentWave;
    private int _totalWaves;

    static public SpawnManager S;

    void Start()
    {
        S = this;
        _currentWave = -1; // avoid off by 1
        _totalWaves = Waves.Length - 1; // adjust, because we're using 0 index

        StartNextWave();
    }

    void StartNextWave()
    {
        _currentWave++;

        // win
        if (_currentWave > _totalWaves)
        {
            return;
        }

        _totalEnemiesInCurrentWave = Waves[_currentWave].EnemiesPerWave;
        _enemiesInWaveLeft = 0;
        _spawnedEnemies = 0;

        StartCoroutine(SpawnEnemies());

    }

    // Coroutine to spawn all of our enemies
    IEnumerator SpawnEnemies()
    {
        //int i = 0;
        HashSet<int> spawnSet = new HashSet<int>();
        for (int i = 0; i < Waves[_currentWave].SpawnPoints.Length; i++)
        {
            spawnSet.Add(i);
        }
        GameObject enemy = Waves[_currentWave].Enemy;
        while (_spawnedEnemies < _totalEnemiesInCurrentWave)
        {
            _spawnedEnemies++;
            _enemiesInWaveLeft++;
            Debug.Log(_enemiesInWaveLeft);

            int item = Random.Range(0, spawnSet.Count);
            int spawnPointIndex = 0;
            int k = 0;
            foreach(int num in spawnSet)
            {
                if (k == item)
                {
                    spawnPointIndex = num;
                    break;
                }
                k++;
            }

            //int spawnPointIndex = Random.Range(0, Waves[_currentWave].SpawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(enemy, Waves[_currentWave].SpawnPoints[spawnPointIndex].position, Waves[_currentWave].SpawnPoints[spawnPointIndex].rotation);
            spawnSet.Remove(spawnPointIndex);
            //i++;
            yield return new WaitForSeconds(Waves[_currentWave].TimeBetweenEnemies);
        }
        yield return null;
    }

    // called by an enemy when they're defeated
    public void EnemyDefeated()
    {
        _enemiesInWaveLeft--;
        Debug.Log(_enemiesInWaveLeft);

        // We start the next wave once we have spawned and defeated them all
        if (_enemiesInWaveLeft == 0 && _spawnedEnemies == _totalEnemiesInCurrentWave)
        {
            StartNextWave();
        }
    }
}