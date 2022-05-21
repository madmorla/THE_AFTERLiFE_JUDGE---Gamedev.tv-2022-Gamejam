using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour, IRaycastable
{

    public bool HandleRaycast(PlayerController callingController)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!callingController.HasGrabbedInRightHand)
            {
                callingController.PickupObject(this.transform);
            }
            else
            {
                callingController.LeaveObject(this.transform);
            }
        }

        return true;
    }

}
