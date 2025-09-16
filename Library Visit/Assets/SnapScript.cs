using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class XRGrabAndSnap : MonoBehaviour
{
    [Header("Snap Settings")]
    public Transform referenceTransform;       // Where to snap
    public float snapDistance = 2f;          // Distance threshold to snap
    public bool disableGrabAfterSnap = true;
   

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnRelease);
        }
    }

    void OnRelease(SelectExitEventArgs args)
    {
        float distance = Vector3.Distance(transform.position, referenceTransform.position);

        if (distance <= snapDistance)
        {
            // Snap to target position and rotation
            transform.SetPositionAndRotation(referenceTransform.position, referenceTransform.rotation);

            // Optional: disable movement and physics
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            // Optional: prevent future grabbing
            if (disableGrabAfterSnap && grabInteractable != null)
            {
                grabInteractable.enabled = false;
            }

         
        }
    }
}