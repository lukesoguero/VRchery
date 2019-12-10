using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 4.0f;
    public float minDist = 5.0f;
    private GameObject objSpawn;
    private int SpawnerID;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        objSpawn = (GameObject)GameObject.FindWithTag("Spawner");
        GameObject temp = GameObject.Find("OVRCameraRig");
        player = temp.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        if (Vector3.Distance(transform.position, player.position) >= minDist)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    // Call this when you want to kill the enemy
    void removeMe()
    {
        objSpawn.BroadcastMessage("killEnemy", SpawnerID);
        Destroy(gameObject);
    }
    // this gets called in the beginning when it is created by the spawner script
    void setName(int sName)
    {
        SpawnerID = sName;
    }
}
