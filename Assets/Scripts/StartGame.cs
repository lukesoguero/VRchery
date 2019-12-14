using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject spawnManager;
    private void OnCollisionEnter(Collision collision)
    {
        spawnManager.GetComponent<SpawnManager>().enabled = true;
        Destroy(gameObject.transform.parent.gameObject);
    }
}
