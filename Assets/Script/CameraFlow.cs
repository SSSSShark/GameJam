using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public float time = 1000;
    public Transform cameraTarget;
    public Vector3 offset = new Vector3(0, 5, -5);
    float rotateTime = 0;

    void Start() {
        transform.position = (Vector3)(cameraTarget.localToWorldMatrix * offset) + cameraTarget.position;
        transform.LookAt(cameraTarget.position);
    }

    void Update()
    {
        Vector3 targetPosition = (Vector3)(cameraTarget.localToWorldMatrix * offset) + cameraTarget.position;
        Quaternion curRotation = transform.rotation;
        transform.LookAt(cameraTarget.position);
        if(rotateTime <= 1)
        {
            transform.rotation = Quaternion.Slerp(curRotation, transform.rotation, Mathf.SmoothStep(0f, 1f, rotateTime));
            transform.position = Vector3.Lerp(transform.position, targetPosition, Mathf.SmoothStep(0f, 1f, rotateTime));
            rotateTime += Time.deltaTime / time;
            Debug.Log(rotateTime);
        }
    }

    public void StartRotate()
    {
        rotateTime = 0;
    }
}
