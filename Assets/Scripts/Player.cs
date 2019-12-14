using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Projectile")
        {
            Die();

        }
        /*if (other.gameObject.tag == "Enemy")
        {
            if (!other.gameObject.GetComponent<Enemy>().dead)
            {
                Die();
            }
            
        }
        else if(other.gameObject.tag == "Projectile")
        {
            Die();
        }*/
    }

    void Die()
    {
        SceneManager.LoadScene("MainScene");
    }
}
