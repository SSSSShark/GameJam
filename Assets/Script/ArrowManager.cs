using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] Communication communicationSO;
    [SerializeField] GameObject up;
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        up.SetActive(communicationSO.availableDirect[0]);
        left.SetActive(communicationSO.availableDirect[2]);
        right.SetActive(communicationSO.availableDirect[3]);
    }
}
