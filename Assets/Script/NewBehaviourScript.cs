using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public List<Trace> centerPoints = new List<Trace>();
    [ContextMenu("random set child")]
    public void SetChild()
    {
        int childCount = transform.childCount;
        Debug.Log("childCount: " + childCount);
        List<GameObject> deleteList = new List<GameObject>();
        for(int i = 0 ; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Vector3 randomOffset = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f) * 3, Random.Range(-1.0f, 1.0f)) * 20;
            Vector3 randomRotate = new Vector3(0, 1, 0) * Random.value * 131;
            child.position += randomOffset;
            child.Rotate(randomRotate);
            if(LeftOrRight(child.position)) deleteList.Add(child.gameObject);
        }
        for(int i = deleteList.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(deleteList[i]);
            // deleteList.RemoveAt(i);
        }
    }
    public bool LeftOrRight(Vector3 pos)
    {
        Vector3 centerVector = centerPoints[1].transform.position - centerPoints[0].transform.position;
        Vector3 toTrain = pos - centerPoints[1].transform.position;
        return Vector3.Cross(centerVector, toTrain).y > 0;
    }
}
