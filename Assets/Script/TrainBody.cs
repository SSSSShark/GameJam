using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainBody : MonoBehaviour
{
    public TrainBody lastTrainBody;
    public TrainBody nextTrainBody;
    public Trace nextTrace;
    public float speed;
    public bool isHead = false;

    void Start() {
        Train train = gameObject.GetComponent<Train>();
        if(train != null)
        {
            nextTrace = train.nextTrace;
            speed = train.speed;
            isHead = true;
        }
    }

    void Update() {
        if(Train.me.communicationSO.gameEnd) return;
        if(nextTrace == null)
            nextTrace = GetLastTarget();
        speed = GetLastSpeed();
        GoForward();
        ListenToTrace();
    }

    Quaternion GetLastRotation()
    {
        if(isHead)
        {
            return GetComponent<Train>().transform.rotation;
        }
        else
        {
            return lastTrainBody.GetLastRotation();
        }
    }

    float GetLastSpeed()
    {
        if(isHead)
        {
            return GetComponent<Train>().speed;
        }
        else
        {
            float lastSpeed = lastTrainBody.GetLastSpeed();
            if((lastTrainBody.transform.position - transform.position).magnitude > 1.2)
            {
                return lastSpeed + 0.1f;
            }
            else
            {
                return lastSpeed - 0.1f;
            }
        }
    }

    public Trace GetLastTarget()
    {
        if(isHead)
        {
            return GetComponent<Train>().nextTrace;
        }
        else
        {
            return lastTrainBody.GetLastTarget();
        }
    }

    void GoForward()
    {
        if(nextTrace == null || isHead) return;
        Vector3 forward = (nextTrace.transform.position - transform.position).normalized;
        transform.position += forward * speed * Time.deltaTime;
    }

    void ListenToTrace()
    {
        if(nextTrace == null) return;
        if((nextTrace.transform.position - transform.position).magnitude < 0.07f)
        {
            if(nextTrainBody != null)
            {
                nextTrainBody.SetTarget(nextTrace);
            }
            SetTarget(GetLastTarget());
        }
    }

    void SetTarget(Trace target)
    {
        nextTrace = target;
        transform.LookAt(target.transform);
    }
}
