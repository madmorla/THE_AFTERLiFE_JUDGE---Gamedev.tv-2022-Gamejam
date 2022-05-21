using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Conveniences;

public class CameraController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private GameObject cam;
    [SerializeField][Range(0, 1000)] private int sensitivity;

    private Vector2 mouseIn;
    private Vector3 rot = new Vector3(0f,0f,0f);

    private void Start()
    {
        Mouse.ToggleCursor(false);
    }
    void Update()
    {
        //Get Mouse Inputs
        mouseIn.x = Input.GetAxis("Mouse X") *  sensitivity * Time.deltaTime;
        mouseIn.y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
    
        //Set rot variables to make them legible for Quaternion
        rot.x -= mouseIn.y;
        rot.y += mouseIn.x;
        rot.x = Mathf.Clamp(rot.x, -5.5f, 22f);
        rot.y = Mathf.Clamp(rot.y, -40f, 40f);

        //Lean effect, it makes it so your z changes depending on your y val
        rot.z = rot.y * 0.15f;

        cam.transform.localRotation = Quaternion.Euler(rot.x, 0,rot.z);
        transform.localRotation = Quaternion.Euler(0,rot.y,0);
    }
}
