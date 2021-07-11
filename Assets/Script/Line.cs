using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Communication communicationSO;
    public float maxOffset;
    public Vector3 oldPos;
    public Vector3 offset;
    public float zr;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        oldPos = parent.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (communicationSO.tugRatio-0.5f) * 2 * maxOffset;
        zr = gameObject.transform.localRotation.z;
        offset = new Vector3(Mathf.Cos(gameObject.transform.localRotation.ToEulerAngles().z), Mathf.Sin(gameObject.transform.localRotation.ToEulerAngles().z), 0) * temp;
        parent.transform.localPosition = oldPos - offset;

    }
}
