using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Conveniences;

public class CameraController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private GameObject cam;
    [SerializeField][Range(0, 1000)] private float sensitivity;
    [SerializeField][Range(-10f, 60f)] private float[] rotxClamp = { 5, 5f, 22f };
    [SerializeField] private float rotyClamp = 70f;

    private Vector2 mouseIn;
    private Vector3 rot = new Vector3(0f, 0f, 0f);

    private float Xsensitivity;
    private float Ysensitivity;

    private void Start()
    {
        Mouse.ToggleCursor(false);

        cam.transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        Xsensitivity = sensitivity;
        Ysensitivity = sensitivity;
    }
    void Update()
    {
        handleRotation();
        smoothRotationClamp();
    }

    private void handleRotation()
    {
        //Get Mouse Inputs
        mouseIn.x = Input.GetAxis("Mouse X") * Xsensitivity * Time.deltaTime;
        mouseIn.y = Input.GetAxis("Mouse Y") * Ysensitivity * Time.deltaTime;

        //Set rot variables to make them legible for Quaternion
        rot.x -= mouseIn.y;
        rot.y += mouseIn.x;
        rot.x = Mathf.Clamp(rot.x, rotxClamp[0], rotxClamp[1]);
        rot.y = Mathf.Clamp(rot.y, -rotyClamp + 1, rotyClamp - 1);

        //Lean effect, it makes it so your z changes depending on your y val
        rot.z = rot.y * 0.15f;

        //Set rots
        cam.transform.localRotation = Quaternion.Euler(rot.x, 0, rot.z);
        transform.localRotation = Quaternion.Euler(0, rot.y, 0);

    }
    private void smoothRotationClamp()
    {
        if (Mathf.Abs(rot.y) > 0)
        {
            //Gets percent of rotation from 0 to the clamp and sets the sensitivity to the sensitivity * left over of percent of 1
            float prcnt = Mathf.Abs(rot.y / rotyClamp);
            float val = 1 - prcnt;
            Xsensitivity = sensitivity * val;
        }
    }
}
