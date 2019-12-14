using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    private int hitPoints = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Arrow(Clone)")
        {
            hitPoints--;
            if (hitPoints > -1)
            {
                gameObject.GetComponentInParent<Animator>().enabled = false;
                Destroy(collision.gameObject);  // Destroy arrow
                if(gameObject.GetComponentInParent<Enemy>())
                {
                    Debug.Log("Enemy body part hit");
                    gameObject.GetComponentInParent<Enemy>().Die();
                }
                else if (gameObject.GetComponentInParent<ShootingEnemy>())
                {
                    Debug.Log("Shooting enemy body part hit");
                    gameObject.GetComponentInParent<ShootingEnemy>().Die();
                }
                Debug.Log("Body part collision: " + gameObject.name);
            }

            //gameObject.GetComponentInParent<Rigidbody>().isKinematic = false;
        }
    }
}
