using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public Communication communicationSO;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = (communicationSO.round / 10).ToString() + (communicationSO.round % 10).ToString() + "/" + (communicationSO.roundMax).ToString();
    }
}
