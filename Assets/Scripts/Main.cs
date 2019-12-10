using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public int numEnemies = 0;
    static public Main S;

    void Start()
    {
        S = this;

        
    }

    void Update()
    {

    }

    public void SpawnEnemy()
    {
        GameObject go = Instantiate<GameObject>(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        numEnemies++;
    }
}
