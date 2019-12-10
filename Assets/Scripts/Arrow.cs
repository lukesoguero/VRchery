using UnityEngine;

public class Arrow : MonoBehaviour
{
   public float speed = 2000.0f;
   public Transform tip = null;

   private Rigidbody rigidbody = null;
   private Vector3 lastPosition = Vector3.zero;
   private bool isStopped = true; 

    private void Awake() {
       rigidbody = GetComponent<Rigidbody>();
   }

   private void FixedUpdate() {
        if (isStopped)
           return;
        // Rotate
        rigidbody.MoveRotation(Quaternion.LookRotation(rigidbody.velocity, transform.up));

        // Collision
        /*
        if (Physics.Linecast(lastPosition, tip.position)) {
           stop();
        }
        */
        // Store position
        lastPosition = tip.position;
   }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isStopped)  // If arrow is flying
        {
            stop();
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
            }
        }
    }
    
    private void stop() {
       isStopped = true;

       rigidbody.isKinematic = true;
       rigidbody.useGravity = false;

   }

   public void fire(float pullValue) {
       isStopped = false;
       transform.parent = null;

       rigidbody.isKinematic = false;
       rigidbody.useGravity = true;
       rigidbody.AddForce(transform.forward * (pullValue * speed));

       Destroy(gameObject, 6.0f);
   }
}

  