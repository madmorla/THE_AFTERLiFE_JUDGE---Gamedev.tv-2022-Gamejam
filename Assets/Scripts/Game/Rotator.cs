using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 dirRotation;
    [SerializeField] private float spinFactor = 0.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(dirRotation * Time.deltaTime * spinFactor); // The main part of the rotation script.
    }
}
