using UnityEngine;

public class DebugPickup : MonoBehaviour
{
    public float pickupDistance = 3f;
    public Transform holdPoint;
    private GameObject heldObject;
    private Rigidbody heldRb;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
                TryPickup();
            else
                Drop();
        }

        if (heldObject)
        {
            heldObject.transform.position = holdPoint.position;
        }
    }

    void TryPickup()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance))
        {
            if (hit.rigidbody != null && hit.rigidbody.isKinematic == false)
            {
                heldObject = hit.collider.gameObject;
                heldRb = heldObject.GetComponent<Rigidbody>();
                heldRb.useGravity = false;
                heldRb.velocity = Vector3.zero;
                heldRb.angularVelocity = Vector3.zero;
                heldRb.isKinematic = true;
            }
        }
    }

    void Drop()
    {
        if (heldRb != null)
        {
            heldRb.useGravity = true;
            heldRb.isKinematic = false;
        }
        heldObject = null;
        heldRb = null;
    }
}
