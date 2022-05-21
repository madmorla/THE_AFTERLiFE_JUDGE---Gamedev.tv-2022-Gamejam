using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform targetRightHand;
    [Range(0.5f, 2)]
    [SerializeField] private float rightHandSpeed = 1f;

    private Camera cam;

    // Take Object with right hand
    private Transform rightHandObject;
    private Vector3 rightHandObject_Initialposition;
    private Quaternion rightHandObject_Initialrotation;
    private bool hasGrabbedInRightHand = false;
    public bool HasGrabbedInRightHand { get => hasGrabbedInRightHand; }


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

    private bool InteractWithObjects()
    {
        //RaycastHit[] hits = RaycastAllSorted();
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (RaycastHit hit in hits)
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

    public void PickupObject(Transform objT)
    {
        StopAllCoroutines();
        StartCoroutine(PickupObjectCoroutine(objT));
    }

    public void LeaveObject(Transform objT)
    {
        StopAllCoroutines();
        StartCoroutine(LeaveObjectCoroutine());
    }

    private IEnumerator PickupObjectCoroutine(Transform objT)
    {
        print("PickingObject " + objT.name);

        hasGrabbedInRightHand = true;

        rightHandObject = objT;
        rightHandObject_Initialposition = objT.position;
        rightHandObject_Initialrotation = objT.rotation;

        yield return InterpolateObject(objT, objT.position, targetRightHand.position, objT.rotation, targetRightHand.rotation);
    }

    private IEnumerator LeaveObjectCoroutine()
    {
        print("LeavingObject " + rightHandObject.name);

        yield return InterpolateObject(rightHandObject, rightHandObject.position, rightHandObject_Initialposition, rightHandObject.rotation, rightHandObject_Initialrotation);
        hasGrabbedInRightHand = false;
        rightHandObject = null;
    }

    IEnumerator InterpolateObject(Transform objT, Vector3 initPos, Vector3 targetPos, Quaternion initRot, Quaternion targetRot)
    {
        bool hasReachedDestination = false;

        // Check if already has reached the destination
        if (Mathf.Approximately(Vector3.Distance(objT.position, targetPos), 0))
        {
            hasReachedDestination = true;
        }

        float t = 0;
        while (!hasReachedDestination)
        {
            t += Time.deltaTime * rightHandSpeed;
            objT.position = Vector3.Lerp(initPos, targetPos, t);
            objT.rotation = Quaternion.Lerp(initRot, targetRot, t);
            if (Mathf.Approximately(Vector3.Distance(objT.position, targetPos), 0))
            {
                hasReachedDestination = true;
            }
            yield return null;
        }
    }
}
