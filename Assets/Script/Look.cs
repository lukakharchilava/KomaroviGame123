using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{

    [SerializeField] float MouseSens = 100f;
    public Transform PlayerBody;
    float xRotation = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        PlayerBody.Rotate(Vector3.up * mouseX);
        
    }
}
