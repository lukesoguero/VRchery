using System.Collections;
using UnityEngine;
using OVRTouchSample;

public class Bow : MonoBehaviour
{
    [Header("Assets")]
    public GameObject arrowPrefab = null;

    [Header("Bow")]
    public float grabThreshold = 0.15f;
    public Transform end = null;
    public Transform start = null;
    public Transform socket = null;
    public Hand hand = null;
    public Arrow currentArrow = null;

    private bool isPulling = false;
    private Animator animator = null;

    private float pullValue = 0.0f;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {

    }

    private void Update() {
        if(!isPulling) return;
        if (!currentArrow)
            lockArrow();
        pullValue = calculatePull(hand.transform);
        pullValue = Mathf.Clamp(pullValue, 0.0f, 1.0f);

        animator.SetFloat("Blend", pullValue);
    }

    public float calculatePull(Transform pullHand) {
        Vector3 direction = end.position - start.position;
        float magnitude = direction.magnitude;
        direction.Normalize();

        Vector3 difference = pullHand.position - start.position;

        return Vector3.Dot(direction, difference) / magnitude;

    }

    private void lockArrow() {
        OVRGrabber handGrabber = hand.GetComponent<OVRGrabber>();
        OVRGrabbable arrowGrabbable = hand.currentArrow.GetComponent<OVRGrabbable>();
        Rigidbody rigidbody = hand.currentArrow.GetComponent<Rigidbody>();
        handGrabber.ForceRelease(arrowGrabbable);
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
        arrowGrabbable.grabPoints = new Collider[0];  // Erase all grabPoints from arrow
        arrowGrabbable.enabled = false;
        hand.currentArrow.transform.parent = socket;
        hand.currentArrow.transform.localPosition = new Vector3(0, 0, 0.33f);
        hand.currentArrow.transform.localEulerAngles = Vector3.zero;
        currentArrow = hand.currentArrow;
        hand.currentArrow = null;
    }

    public void pull() {
        float distance = Vector3.Distance(hand.transform.position, start.position);
        if (distance > grabThreshold) return;
        isPulling = true;
    }

    public void release() {
        if (pullValue > 0.25f) {
            fireArrow();
        }
        pullValue = 0.0f;
        animator.SetFloat("Blend", 0.0f);
        isPulling = false;
    }

    private void fireArrow() {
        currentArrow.fire(pullValue);
        currentArrow = null;
    }
}
