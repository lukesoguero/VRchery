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
    

    private void Update()
    {
        OVRInput.Update();
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown("space")) {
            // If player is reaching into quiver
            if (quiver.GetComponent<Collider>().bounds.Contains(hand.transform.position)) {
                hand.createArrow();
            }
        }

        if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKey("space")) {
            bow.pull();
        }

        if (Input.GetKeyUp("space") || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)) {
            bow.release();
        }
    }
}
