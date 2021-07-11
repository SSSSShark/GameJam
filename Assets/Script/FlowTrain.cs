using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowTrain : MonoBehaviour
{
    [SerializeField]
    bool posFlow;
    [SerializeField]
    bool rotateFlow;
    void Update() {
        if(posFlow) PositionFlow();
        if(rotateFlow) RotationFlow();
    }

    void PositionFlow()
    {
        Vector3 pos = transform.position;
        Vector3 trainPos = Train.me.transform.position;
        pos.x = trainPos.x;
        pos.z = trainPos.z;
        transform.position = pos;
    }

    void RotationFlow()
    {
        Vector3 rotEular = transform.rotation.eulerAngles;
        rotEular.y = Train.me.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(rotEular);
    }
}
