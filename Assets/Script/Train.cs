using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public enum Direction
    {
        left,
        forward,
        right,
        random
    }

    CameraFlow cameraFlow;
    public float speed;
    Trace nextTrace;
    Direction nextDir = Direction.random;
    void Awake() {
        nextTrace = FindObjectOfType<Trace>();
        cameraFlow = FindObjectOfType<CameraFlow>();
    }
    void Update()
    {
        GoForward();
        ListenToTrace();
    }

    void GoForward()
    {
        Vector3 forward = (nextTrace.transform.position - transform.position).normalized;
        transform.position += forward * speed * Time.deltaTime;
    }

    public void SetDir(Direction dir)
    {
        nextDir = dir;
    }
    void SetTarget(Trace target)
    {
        nextTrace = target;
        transform.LookAt(target.transform);
        cameraFlow.StartRotate();
    }

    void ListenToTrace()
    {
        if((nextTrace.transform.position - transform.position).magnitude < 0.01)
            SetTarget(nextTrace.GetNext(nextDir));
    }
}
