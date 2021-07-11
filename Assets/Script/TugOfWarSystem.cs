using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TugOfWarSystem : MonoBehaviour
{
    [Header("胜利阈值")]
    [SerializeField] int maxTug = 10;
    [Header("最长时长")]
    [SerializeField] float time = 5;
    [Space(30)]
    [SerializeField] GameObject tugUI;
    [SerializeField] Communication communicationSO;
    [SerializeField] private int tugging;
    private float timeAcc;
    private bool showFlag;
    [SerializeField] private float showTime = 1.0f;
    private float showTimeAcc;

    [SerializeField] GameObject Awin;
    [SerializeField] GameObject Bwin;


    // Start is called before the first frame update
    void Start()
    {
        tugUI.SetActive(false);
        Awin.SetActive(false);
        Bwin.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (communicationSO.GMToTugSystem&&!showFlag)
        {
            if (!tugUI.activeSelf)
            {
                tugUI.SetActive(true);
                tugging = 0;
                timeAcc = 0;
                communicationSO.tugOfWar = true;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                tugging++;
                communicationSO.isPressSpace = true;
            }
            else
            {
                communicationSO.isPressSpace = false;
            }
            if (Input.GetKeyDown(KeyCode.KeypadPeriod))
            {
                tugging--;
                communicationSO.isPressDot = true;
            }
            else
            {
                communicationSO.isPressDot = false;
            }
            communicationSO.tugRatio = ((float)tugging + maxTug) / (2f * maxTug);
            timeAcc += Time.deltaTime;
            if (timeAcc >= time || tugging >= 10 || tugging <= -10)
            {
                communicationSO.tugResult = (tugging > 0) ? 1 : ((tugging < 0) ? -1 : 0);
                showTimeAcc = 0;
                showFlag = true;
                Awin.SetActive(communicationSO.tugResult > 0);
                Bwin.SetActive(communicationSO.tugResult < 0);
            }
        }
        else if (showFlag)
        {
            showTimeAcc += Time.deltaTime;
            if (showTimeAcc >= showTime)
            {
                communicationSO.tugSystemToGM = true;
                communicationSO.tugOfWar = false;
                communicationSO.GMToTugSystem = false;
                Awin.SetActive(false);
                Bwin.SetActive(false);
                tugUI.SetActive(false);
                showFlag = false;
            }
        }
    }
}
