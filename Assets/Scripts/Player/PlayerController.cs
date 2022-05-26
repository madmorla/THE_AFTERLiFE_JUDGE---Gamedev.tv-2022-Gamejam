using Conveniences;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform targetRightHand;
    public Transform TargetRightHand { get => targetRightHand; }

    [Range(0.5f, 3)]
    [SerializeField] private float rightHandSpeed = 2f;
    public float RightHandSpeed { get => rightHandSpeed; }

    private Camera cam;

    // Take Object with right hand
    [SerializeField] private PickableObject rightHandObject;
    public PickableObject RightHandObject { get => rightHandObject; set => rightHandObject = value; }


    //----------------------------------
    // Unity Methods

    void Awake()
    {
        cam = Camera.main;
        Mouse.ToggleCursor(false);
    }

    void Update()
    {
        if (InteractWithObjects())
        {
            return;
        }
    }

    //----------------------------------
    // Interaction with IRaycastable objects with mouse

    private bool InteractWithObjects()
    {
        // Cast a ray from the mouse in camera direction and get the first hit
        RaycastHit hit;
        if (Physics.Raycast(GetMouseRay(), out hit))
        {
            // Get all the IRaycastable components from the hit object
            IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
            foreach (IRaycastable raycastable in raycastables)
            {
                // Execute the handle function implemented for that IRaycastable object
                if (raycastable.HandleRaycast(this))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }

}
