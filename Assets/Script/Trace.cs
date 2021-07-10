using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace : MonoBehaviour
{
    enum Direction
    {
        east,
        west,
        south,
        north
    }
    Vector3[] directionVectors =
    {
        Vector3.right,
        Vector3.left,
        Vector3.back,
        Vector3.forward
    };
    private List<KeyValuePair<Direction, Trace>> others;

    void Start() {
        others = new List<KeyValuePair<Direction, Trace>>();
        for(int i = 0 ; i < 4; i++)
        {
            if(Physics.Raycast(transform.position, directionVectors[i], out RaycastHit hit, 10000))
            {
                Trace hitTrace = hit.transform.GetComponent<Trace>();
                if(hitTrace)
                {
                    Direction dirKey = VectorToDirection(directionVectors[i]);
                    KeyValuePair<Direction, Trace> vt = new KeyValuePair<Direction, Trace>(dirKey, hitTrace);
                    others.Add(vt);
                }
            }
        }
    }

    Direction VectorToDirection(Vector3 vec)
    {
        int minIndex = 0;
        float minDiff = float.MaxValue;
        for(int i = 0; i < 4; i++)
        {
            float diff = (directionVectors[i] - vec).sqrMagnitude;
            if(minDiff > diff)
            {
                minDiff = diff;
                minIndex = i;
            }
        }
        Direction dirKey = (Direction)minIndex;
        return dirKey;
    }

    Direction TrunTo(Direction from, Train.Direction dir)
    {
        if(dir == Train.Direction.random)
        {
            Train.Direction rand = (Train.Direction)(Random.Range(0, 3));
            return TrunTo(from, rand);
        }
        Direction to = Direction.north;
        switch(from)
        {
            case Direction.east:
                switch(dir)
                {
                    case Train.Direction.left:
                        to = Direction.south;
                        break;
                    case Train.Direction.right:
                        to = Direction.north;
                        break;
                    case Train.Direction.forward:
                        to = Direction.west;
                        break;
                }
            break;
            case Direction.west:
                switch(dir)
                {
                    case Train.Direction.left:
                        to = Direction.north;
                        break;
                    case Train.Direction.right:
                        to = Direction.south;
                        break;
                    case Train.Direction.forward:
                        to = Direction.east;
                        break;
                }
            break;
            case Direction.south:
                switch(dir)
                {
                    case Train.Direction.left:
                        to = Direction.west;
                        break;
                    case Train.Direction.right:
                        to = Direction.east;
                        break;
                    case Train.Direction.forward:
                        to = Direction.north;
                        break;
                }
            break;
            case Direction.north:
                switch(dir)
                {
                    case Train.Direction.left:
                        to = Direction.east;
                        break;
                    case Train.Direction.right:
                        to = Direction.west;
                        break;
                    case Train.Direction.forward:
                        to = Direction.south;
                        break;
                }
            break;
        }
        Debug.Log("to:" + to);
        return to;
    }
    Train.Direction GetTrainDir(Direction from, Direction dir)
    {
        Train.Direction to = Train.Direction.random;
        switch(from)
        {
            case Direction.east:
                switch(dir)
                {
                    case Direction.south:
                        to = Train.Direction.left;
                        break;
                    case Direction.north:
                        to = Train.Direction.right;
                        break;
                    case Direction.west:
                        to = Train.Direction.forward;
                        break;
                }
            break;
            case Direction.west:
                switch(dir)
                {
                    case Direction.north:
                        to = Train.Direction.left;
                        break;
                    case Direction.south:
                        to = Train.Direction.right;
                        break;
                    case Direction.east:
                        to = Train.Direction.forward;
                        break;
                }
            break;
            case Direction.south:
                switch(dir)
                {
                    case Direction.west:
                        to = Train.Direction.left;
                        break;
                    case Direction.east:
                        to = Train.Direction.right;
                        break;
                    case Direction.north:
                        to = Train.Direction.forward;
                        break;
                }
            break;
            case Direction.north:
                switch(dir)
                {
                    case Direction.east:
                        to = Train.Direction.left;
                        break;
                    case Direction.west:
                        to = Train.Direction.right;
                        break;
                    case Direction.south:
                        to = Train.Direction.forward;
                        break;
                }
            break;
        }
        return to;
    }

    public Trace GetNext(Train.Direction dir)
    {
        Vector3 traceToTrain = (Train.me.transform.position - transform.position).normalized;
        Direction from = VectorToDirection(traceToTrain);
        Direction to = TrunTo(from, dir);
        foreach(var other in others)
        {
            if(to == other.Key)
            {
                return other.Value;
            }
        }
        return GetRandNext(from);
    }

    public List<Train.Direction> GetCanGo()
    {
        Vector3 traceToTrain = (Train.me.transform.position - transform.position).normalized;
        Direction from = VectorToDirection(traceToTrain);
        List<Train.Direction> goList = new List<Train.Direction>();
        foreach(var pair in others)
        {
            if(pair.Key == from) continue;
            else goList.Add(GetTrainDir(from, pair.Key));
        }
        return goList;
    }

    Trace GetRandNext(Direction from)
    {
        int index = Random.Range(0, others.Count);
        KeyValuePair<Direction, Trace> next = others[index];
        if(next.Key == from)
        {
            index = (index + 1) % others.Count;
        }
        return others[index].Value;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        Train.me.StartEnergyCompete();
        Train.me.speed = 0;
    }
    private Vector3 Rotate(Vector3 source, Vector3 axis, float angle)
    {
        Quaternion q = Quaternion.AngleAxis(angle, axis);// 旋转系数
        return q * source;// 返回目标点
    }

}
