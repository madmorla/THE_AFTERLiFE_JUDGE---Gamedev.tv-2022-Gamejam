using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour, IRaycastable
{
    Vector3 initialPos;
    Quaternion initialRot;

    public Vector3 InitialPos { get => initialPos; }
    public Quaternion InitialRot { get => initialRot; }

    public bool HandleRaycast(PlayerController callingController)
    {
        //TODO: Fix This
        // Get all to this script, interaction, etc...
        if (Input.GetMouseButtonDown(0))
        {
            if (!callingController.RightHandObject)
            {
                SetInitialState();
                callingController.PickupObject(this);
            }
            else
            {
                callingController.LeaveObject();
                if (callingController.RightHandObject != this)
                {
                    callingController.LeaveObject();
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

}
