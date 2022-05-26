using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour, IRaycastable
{

    [Header("SFX")]
    [SerializeField] private AudioClip sfx_pickupObj;
    [SerializeField] private float sfx_pickupObj_delay = 0f;
    [SerializeField] private AudioClip sfx_leaveObj;
    [SerializeField] private float sfx_leaveObj_delay = 0f;

    private AudioSource audioSource;
    private Vector3 initialPos;
    private Quaternion initialRot;
    private bool pickingFinished = true;

    public Vector3 InitialPos { get => initialPos; }
    public Quaternion InitialRot { get => initialRot; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool HandleRaycast(PlayerController callingController)
    {
        // When you click left mouse button then try to pickup a object
        if (Input.GetMouseButtonDown(0))
        {
            // If don't have an object picked, then pickup
            if (!callingController.RightHandObject)
            {
                SetInitialState();
                PickupObject(this, callingController);
            }
            else
            {
                // If there is an object picked, then leave it
                LeaveObject(callingController);

                // If you want to pick another object, but you have another picked,
                // then leave the picked one
                if (callingController.RightHandObject != this)
                {
                    LeaveObject(callingController);
                }
            }
        }

        return true;
    }

    void SetInitialState()
    {
        initialPos = this.transform.position;
        initialRot = this.transform.rotation;
    }


    //----------------------------------
    // Pickup Objects Methods

    public void PickupObject(PickableObject pickableObj, PlayerController callingController)
    {
        if (pickingFinished)
        {
            StartCoroutine(PickupObjectCoroutine(pickableObj, callingController));
        }
    }

    public void LeaveObject(PlayerController callingController)
    {
        if (pickingFinished)
        {
            StartCoroutine(LeaveObjectCoroutine(callingController));
        }
    }

    private IEnumerator PickupObjectCoroutine(PickableObject pickableObj, PlayerController callingController)
    {
        //print("PickingObject " + pickableObj.name);

        if (sfx_pickupObj)
        {
            audioSource.clip = sfx_pickupObj;
            audioSource.PlayDelayed(sfx_pickupObj_delay);
        }

        pickingFinished = false;
        callingController.RightHandObject = pickableObj;

        Transform initT = callingController.RightHandObject.transform;
        Transform targetT = callingController.TargetRightHand;

        yield return InterpolateObject(initT,
                        initT.position, targetT.position,
                        initT.rotation, targetT.rotation,
                        callingController.RightHandSpeed);
        pickingFinished = true;
    }

    private IEnumerator LeaveObjectCoroutine(PlayerController callingController)
    {
        //print("LeavingObject " + callingController.RightHandObject.name);

        if (sfx_pickupObj)
        {
            audioSource.clip = sfx_leaveObj;
            audioSource.PlayDelayed(sfx_leaveObj_delay);
        }

        pickingFinished = false;

        Transform initT = callingController.RightHandObject.transform;

        yield return InterpolateObject(initT,
                        initT.position, callingController.RightHandObject.InitialPos,
                        initT.rotation, callingController.RightHandObject.InitialRot,
                        callingController.RightHandSpeed);

        pickingFinished = true;
        callingController.RightHandObject = null;
    }

    private IEnumerator InterpolateObject(Transform objT, Vector3 initPos, Vector3 targetPos, Quaternion initRot, Quaternion targetRot, float speed)
    {
        float t = 0;
        while (!HasReachedDestination(objT.position, targetPos))
        {
            t += Mathf.Clamp01(Time.deltaTime * speed);
            objT.position = Vector3.Lerp(initPos, targetPos, t);
            objT.rotation = Quaternion.Lerp(initRot, targetRot, t);

            yield return null;
        }
    }

    private bool HasReachedDestination(Vector3 currentPos, Vector3 targetPos)
    {
        return Mathf.Approximately(Vector3.Distance(currentPos, targetPos), 0);
    }

}
