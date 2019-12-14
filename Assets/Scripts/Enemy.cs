using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 4.0f;
    public float minDist = 5.0f;
    private GameObject objSpawn;
    private int SpawnerID;
    public bool dead = false;

    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        objSpawn = (GameObject)GameObject.FindWithTag("Spawner");
        player = GameObject.Find("CenterEyeAnchor");
        //player = temp.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        //player = GameObject.FindWithTag("Player").transform;
        transform.LookAt(playerPos);
        
        if (Vector3.Distance(transform.position, playerPos) >= minDist && !dead)
        {
            //Vector3 unit = new Vector3(0, 1, 0);
            transform.position += transform.forward * speed * Time.deltaTime;
            //p.y = 0;
        }
        
    }

    public void Die()
    {
        if (!dead)
        {
            dead = true;
            Destroy(gameObject, 5);
            SpawnManager.S.EnemyDefeated();
        }
    }
    // Call this when you want to kill the enemy
/*    public void Die()
    {
        objSpawn.BroadcastMessage("killEnemy", SpawnerID);
        
        Destroy(gameObject, 5);
        bool dead = true;
        Debug.Log("Enemy died");
    }
    // this gets called in the beginning when it is created by the spawner script
    void setName(int sName)
    {
        SpawnerID = sName;
    }*/
}
