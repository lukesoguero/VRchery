using UnityEngine;
using System.Collections;

// Applies an explosion force to all nearby rigidbodies
public class Explosive : MonoBehaviour
{
    public float radius = 2.0F;
    public float power = 10.0F;
    public GameObject explosionEffect = null;

    private bool exploded = false;

    protected float m_timer = 0.0f;
    protected bool m_isTimerSet = false;

    public float timer
    {
        get { return timer; }
        set { m_timer = value; }
    }

    public bool isTimerSet
    {
        get { return isTimerSet; }
        set { m_isTimerSet = value; }
    }

    private void Update()
    {
        if (m_isTimerSet)
        {
            m_timer -= Time.deltaTime;
            if (m_timer <= 0.0f)
                Explode();
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.name == "Arrow(Clone)")
        {
            m_isTimerSet = true;  // First bomb should explode immediately, m_timer defaults to 0.0 seconds
            Destroy(coll.gameObject);
        }
            
    }

    public void Explode()
    {
        if (m_isTimerSet && m_timer <= 0.0f)
        {
            exploded = true;
            Vector3 explosionPos = transform.position;
            GameObject effect = Instantiate(explosionEffect, explosionPos, transform.rotation);
            Destroy(effect, 1.7f);
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                if (hit.gameObject.tag == "Enemy" && hit.gameObject.GetComponentInParent<Enemy>().dead == false)
                {
                    hit.gameObject.GetComponentInParent<Animator>().enabled = false;
                    if (hit.gameObject.GetComponentInParent<Enemy>())
                    {
                        hit.gameObject.GetComponentInParent<Enemy>().Die();
                    }
                    else if (hit.gameObject.GetComponentInParent<ShootingEnemy>())
                    {
                        hit.gameObject.GetComponentInParent<ShootingEnemy>().Die();
                    }

                }

                if (hit.gameObject.tag == "Explosive" && hit.transform != transform)
                {
                    if (hit.gameObject.GetComponent<Explosive>().exploded != true)
                    {
                        Debug.Log((transform.position - hit.transform.position).magnitude / 7.0f);
                        hit.gameObject.GetComponent<Explosive>().timer = (transform.position - hit.transform.position).magnitude/15.0f; // Timer is proportional to distance between two bombs
                        hit.gameObject.GetComponent<Explosive>().isTimerSet = true;
                    }

                }

                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 0.5f);
            }
            Destroy(gameObject);
        }
    }


}
