using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    static public Train me;
    public enum Direction
    {
        left,
        forward,
        right,
        random
    }

    CameraFlow cameraFlow;
    public float speed = 10;
    Trace nextTrace;
    Direction nextDir = Direction.random;
    EnergySystem energySystem;
    void Awake() {
        nextTrace = FindObjectOfType<Trace>();
        cameraFlow = FindObjectOfType<CameraFlow>();
        energySystem = FindObjectOfType<EnergySystem>();
        me = this;
    }

    void Start() {
        SetTarget(nextTrace);
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
        speed = 10;
    }
    void SetTarget(Trace target)
    {
        nextTrace = target;
        transform.LookAt(target.transform);
        cameraFlow.StartRotate();
    }

    public void StartEnergyCompete()
    {
        bool[] button = {true, false, true, true};
        energySystem.StartEnergyCompete(button);
    }

    void ListenToTrace()
    {
        if((nextTrace.transform.position - transform.position).magnitude < 0.01)
            SetTarget(nextTrace.GetNext(nextDir));
    }
}
