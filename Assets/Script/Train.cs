using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField]
    Communication communicationSO;
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
    float speedOld;
    public Trace nextTrace;
    Direction nextDir = Direction.random;
    EnergySystem energySystem;
    void Awake() {
        //nextTrace = FindObjectOfType<Trace>();
        cameraFlow = FindObjectOfType<CameraFlow>();
        energySystem = FindObjectOfType<EnergySystem>();
        me = this;
    }

    void Start() {
        speedOld = speed;
        SetTarget(nextTrace);
    }
    void Update()
    {
        GoForward();
        ListenToTrace();
        if(communicationSO.competeEnergy || communicationSO.tugOfWar)
        {
            if(communicationSO.tugOfWar)
                Stop();
            else
                Stop(0.5f);
        }
        else
        {
            ResetSpeed();
        }

    }

    void GoForward()
    {
        Vector3 forward = (nextTrace.transform.position - transform.position).normalized;
        transform.position += forward * speed * Time.deltaTime;
    }

    public void SetDir(Direction dir)
    {
        nextDir = dir;
        speed = speedOld;
    }
    void SetTarget(Trace target)
    {
        nextTrace = target;
        transform.LookAt(target.transform);
        cameraFlow.StartRotate();
    }

    public void StartEnergyCompete()
    {
        bool[] button = {false, false, false, false};
        List<Direction> canGoes = nextTrace.GetCanGo();
        foreach(var canGo in canGoes)
        {
            switch(canGo)
            {
                case Direction.forward:
                    button[0] = true;
                    break;
                case Direction.left:
                    button[2] = true;
                    break;
                case Direction.right:
                    button[3]= true;
                    break;
            }
        }
        energySystem.StartEnergyCompete(button);
    }

    void ListenToTrace()
    {
        if((nextTrace.transform.position - transform.position).magnitude < 0.07)
            {
                SetTarget(nextTrace.GetNext(nextDir));
                //transform.position = nextTrace.transform.position;
            }

    }

    public void Stop(float s = 0)
    {
        // speedOld = speed;
        speed = s;
    }

    public void ResetSpeed()
    {
        speed = speedOld;
    }
}
