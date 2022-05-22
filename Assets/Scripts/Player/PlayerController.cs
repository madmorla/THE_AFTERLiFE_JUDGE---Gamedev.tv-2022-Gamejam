using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform targetRightHand;
    public Transform TargetRightHand { get => targetRightHand; }

    [Range(0.5f, 3)]
    [SerializeField] private float rightHandSpeed = 2f;

    private Camera cam;

    // Take Object with right hand
    [SerializeField] private PickableObject rightHandObject;
    public PickableObject RightHandObject { get => rightHandObject; }

    private Vector3 rightHandObject_Initialposition;
    private Quaternion rightHandObject_Initialrotation;
    private bool pickingFinished = true;


    //----------------------------------
    // Unity Methods

    void Awake()
    {
        cam = Camera.main;
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
        RaycastHit hit;
        if (Physics.Raycast(GetMouseRay(), out hit))
        {
            IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
            foreach (IRaycastable raycastable in raycastables)
            {
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


    //----------------------------------
    // Pickup Objects Methods

    public void PickupObject(PickableObject pickableObj)
    {
        if (!rightHandObject && pickingFinished)
        {
            StartCoroutine(PickupObjectCoroutine(pickableObj));
        }
    }

    public void LeaveObject()
    {
        if (rightHandObject && pickingFinished)
        {
            StartCoroutine(LeaveObjectCoroutine());
        }
    }

    private IEnumerator PickupObjectCoroutine(PickableObject pickableObj)
    {
        print("PickingObject " + pickableObj.name);
        pickingFinished = false;
        rightHandObject = pickableObj;

        yield return InterpolateObject(rightHandObject.transform,
                        rightHandObject.transform.position, targetRightHand.position,
                        rightHandObject.transform.rotation, targetRightHand.rotation);
        pickingFinished = true;

    }

    private IEnumerator LeaveObjectCoroutine()
    {
        print("LeavingObject " + rightHandObject.name);
        pickingFinished = false;

        yield return InterpolateObject(rightHandObject.transform,
                        rightHandObject.transform.position, rightHandObject.InitialPos,
                        rightHandObject.transform.rotation, rightHandObject.InitialRot);
        pickingFinished = true;
        rightHandObject = null;
    }

    private IEnumerator InterpolateObject(Transform objT, Vector3 initPos, Vector3 targetPos, Quaternion initRot, Quaternion targetRot)
    {
        float t = 0;
        while (!HasReachedDestination(objT.position, targetPos))
        {
            t += Time.deltaTime * rightHandSpeed;
            objT.position = Vector3.Lerp(initPos, targetPos, t);
            objT.rotation = Quaternion.Lerp(initRot, targetRot, t);

            yield return null;
        }
    }

    private bool HasReachedDestination(Vector3 currentPos, Vector3 targetPos)
    {
        return Mathf.Approximately(Vector3.Distance(currentPos, targetPos), 0);
    }

    //----------------------------------

}
