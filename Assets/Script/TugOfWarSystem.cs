using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TugOfWarSystem : MonoBehaviour
{
    [Header("胜利阈值")]
    [SerializeField] int maxTug = 10;
    [Header("最长时长")]
    [SerializeField] int time = 5;
    [Space(30)]
    [SerializeField] GameObject tugUI;
    [SerializeField] Communication communicationSO;
    private int tugging;
    private float timeAcc;

    // Start is called before the first frame update
    void Start()
    {
        tugUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (communicationSO.GMToTugSystem)
        {
            if (!tugUI.activeSelf)
            {
                tugUI.SetActive(true);
                tugging = 0;
                timeAcc = 0;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                tugging++;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                tugging--;
            }
            communicationSO.tugRatio = ((float)tugging + maxTug) / (2f * maxTug);
            timeAcc += Time.deltaTime;
            if (timeAcc >= time || tugging >= 10 || tugging <= -10)
            {
                communicationSO.tugResult = (tugging > 0) ? 1 : ((tugging < 0) ? -1 : 0);
                communicationSO.tugSystemToGM = true;
                communicationSO.GMToTugSystem = false;
                tugUI.SetActive(false);
            }
        }
    }
}
