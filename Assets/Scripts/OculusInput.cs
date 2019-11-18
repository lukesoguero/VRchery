using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;
// using OVR;

public class OculusInput : MonoBehaviour
{
    public Bow bow = null;
    public Hand hand = null; 
    public GameObject quiver = null;
    public OVRInput.Controller controller = OVRInput.Controller.None;
    

    private void Update()
    {
        OVRInput.Update();
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller) || Input.GetKeyDown("space")) {
            Debug.Log("Pressing");
            // If player is reaching into quiver
            if (quiver.GetComponent<Collider>().bounds.Contains(hand.transform.position)) {
                Debug.Log("In quiver");
                hand.createArrow();
            }
        }

        if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controller) || Input.GetKey("space")) {
            bow.pull();
        }

        if (Input.GetKeyUp("space") || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)) {
            bow.release();
        }
    }
}
