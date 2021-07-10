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

    [SerializeField]
    public List<Trace> otherTraces;
    private List<KeyValuePair<Direction, Trace>> others;

    void Start() {
        others = new List<KeyValuePair<Direction, Trace>>();
        foreach(var oTrace in otherTraces)
        {
            Vector3 keyVector = (oTrace.transform.position - transform.position).normalized;
            Direction dirKey = VectorToDirection(keyVector);
            Trace value = oTrace;
            KeyValuePair<Direction, Trace> vt = new KeyValuePair<Direction, Trace>(dirKey, value);
            others.Add(vt);
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
            Debug.Log(rand);
            return TrunTo(from, rand);
        }
        Direction to = Direction.north;
        switch(from)
        {
            case Direction.east:
                switch(dir)
                {
                    case Train.Direction.left:
                        return Direction.south;
                    case Train.Direction.right:
                        return Direction.north;
                    case Train.Direction.forward:
                        return Direction.west;
                }
            break;
            case Direction.west:
                switch(dir)
                {
                    case Train.Direction.left:
                        return Direction.north;
                    case Train.Direction.right:
                        return Direction.south;
                    case Train.Direction.forward:
                        return Direction.east;
                }
            break;
            case Direction.south:
                switch(dir)
                {
                    case Train.Direction.left:
                        return Direction.west;
                    case Train.Direction.right:
                        return Direction.east;
                    case Train.Direction.forward:
                        return Direction.north;
                }
            break;
            case Direction.north:
                switch(dir)
                {
                    case Train.Direction.left:
                        return Direction.east;
                    case Train.Direction.right:
                        return Direction.west;
                    case Train.Direction.forward:
                        return Direction.south;
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

    Trace GetRandNext(Direction from)
    {
        int index = (Random.Range(0, 4)) % others.Count;
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
