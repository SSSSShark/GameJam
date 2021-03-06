using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField]
    public Communication communicationSO;
    static public Train me;
    public enum Direction
    {
        left,
        forward,
        right,
        random
    }
    public List<Trace> centerPoints = new List<Trace>();
    CameraFlow cameraFlow;
    public float speed = 10;
    float speedOld;
    public Trace nextTrace;
    Direction nextDir = Direction.random;
    EnergySystem energySystem;
    bool competeFlag = false;
    float speedTimer = 0;
    public float aTime = 0.7f;
    bool isFirst = true;
    void Awake() {
        //nextTrace = FindObjectOfType<Trace>();
        cameraFlow = FindObjectOfType<CameraFlow>();
        energySystem = FindObjectOfType<EnergySystem>();
        me = this;
    }

    void Start() {
        speedOld = speed;
        speed = 2;
        SetTarget(nextTrace);
    }
    void Update()
    {
        if(communicationSO.gameEnd) return;
        GoForward();
        ListenToTrace();
        if(communicationSO.competeEnergy || communicationSO.tugOfWar)
        {
            if(communicationSO.tugOfWar)
                Stop();
            else
                Stop(1.0f);
            competeFlag = true;
        }
        else
        {
            if(competeFlag)
            {
                ResetSpeed();
                competeFlag = false;
            }

        }

    }

    void GoForward()
    {
        Vector3 forward = (nextTrace.transform.position - transform.position).normalized;
        transform.position += forward * speed * Time.deltaTime;
    }

    public void SetDir(Direction dir)
    {
        Debug.Log(dir);
        nextDir = dir;
        ResetSpeed();
    }
    void SetTarget(Trace target)
    {
        nextTrace = target;
        transform.LookAt(target.transform);
        cameraFlow.StartRotate();
    }

    public void StartEnergyCompete()
    {
        if(isFirst)
        {
            communicationSO.GMToTugSystem = true;
            isFirst = false;
            return;
        }
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
        if((nextTrace.transform.position - transform.position).magnitude < 0.07f)
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
        StartCoroutine(SpeedAdd());
    }


    IEnumerator SpeedAdd()
    {
        for(speedTimer = 0; speedTimer < aTime; speedTimer += Time.deltaTime)
        {
            speed = Mathf.Lerp(1, speedOld, Mathf.SmoothStep(0, 1, speedTimer / aTime));
            yield return null;
        }
        speed = speedOld;
    }


    public bool LeftOrRight()
    {
        Vector3 centerVector = centerPoints[1].transform.position - centerPoints[0].transform.position;
        Vector3 toTrain = transform.position - centerPoints[1].transform.position;
        return Vector3.Cross(centerVector, toTrain).y > 0;
    }
}
