using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTug : MonoBehaviour
{
    [SerializeField] Communication communitcationSO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = (communitcationSO.tugRatio * 10 - 10).ToString();
    }
}
