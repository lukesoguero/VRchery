using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    private GameObject objSpawn;
    private int SpawnerID;
    public bool dead = false;

    GameObject player;

    [SerializeField]
    GameObject projectile;

    float fireRate;
    float nextFire;

    Vector3 projPos;


    // Start is called before the first frame update
    void Start()
    {
        fireRate = 6f;
        nextFire = Time.time;
        objSpawn = (GameObject)GameObject.FindWithTag("Spawner");
        player = GameObject.Find("CenterEyeAnchor");
        projPos = new Vector3 (transform.position.x, transform.position.y + 1.25f, transform.position.z);
        //player = temp.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        //player = GameObject.FindWithTag("Player").transform;
        transform.LookAt(playerPos);
        CheckIfTimeToFire();
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

    public void Fire()
    {

    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire && !dead)
        {
            Instantiate(projectile, projPos, transform.rotation);
            nextFire = Time.time + fireRate;
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
