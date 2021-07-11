using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("最大回合数")]
    [SerializeField] private int roundNum = 20;
    [SerializeField] Communication communicationSO;
    int round;
    [SerializeField] private Train train;
    [SerializeField] private GameObject AWin;
    [SerializeField] private GameObject BWin;
    [SerializeField] Color A;
    [SerializeField] Color B;

    [SerializeField] Color common1;
    [SerializeField] Color common2;


    [SerializeField] Material x2;
    [SerializeField] Material train1;
    [SerializeField] Material train3;

    public void AWinGame()
    {
        AWin.SetActive(true);
        communicationSO.gameEnd = true;
        Debug.Log("AWin");
    }
    public void BWinGame()
    {
        BWin.SetActive(true);
        communicationSO.gameEnd = true;
        Debug.Log("BWin");
    }

    public void Restart()
    {
        EditorSceneManager.LoadScene(1);
    }

    public void Back()
    {
        EditorSceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        communicationSO.round = round = 0;
        communicationSO.roundMax = roundNum;
        train = FindObjectOfType<Train>();
        AWin.SetActive(false);
        BWin.SetActive(false);
        communicationSO.gameEnd = false;
        communicationSO.result = 0;
        ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (round >= roundNum)
        {
            if (train.LeftOrRight())
            {
                AWinGame();
            }
            else
            {
                BWinGame();
            }

            communicationSO.gameEnd = true;
        }
    }

    void ChangeColor()
    {
        x2.SetColor("_EmissionColor", (communicationSO.result > 0) ? A : ((communicationSO.result < 0) ? B : common1));
        train3.SetColor("_EmissionColor", (communicationSO.result > 0) ? A : ((communicationSO.result < 0) ? B : common1));
        train1.SetColor("_Color", (communicationSO.result > 0) ? A : ((communicationSO.result < 0) ? B : common2));
    }

    private void LateUpdate()
    {
        if (communicationSO.energySystemToGM)
        {
            communicationSO.playerAisBet = communicationSO.playerADirect >= 0 && communicationSO.playerADirect <= 3;
            communicationSO.playerBisBet = communicationSO.playerBDirect >= 0 && communicationSO.playerBDirect <= 3;

            if (communicationSO.playerAisBet || communicationSO.playerBisBet)
            {
                communicationSO.result = communicationSO.playerABet - communicationSO.playerBBet;
                if (communicationSO.result != 0)
                {
                    communicationSO.playerAReturn = (communicationSO.result > 0) ? 0 : Mathf.Max(0, communicationSO.playerABet - 1);
                    communicationSO.playerBReturn = (communicationSO.result < 0) ? 0 : Mathf.Max(0, communicationSO.playerBBet - 1);
                    communicationSO.GMToEnergySystem = true;
                    ChangeColor();
                }
                else
                {
                    communicationSO.GMToTugSystem = true;
                }
            }
            else
            {
                communicationSO.result = 0;
                communicationSO.playerAReturn = (communicationSO.result > 0) ? 0 : Mathf.Max(0, communicationSO.playerABet - 1);
                communicationSO.playerBReturn = (communicationSO.result < 0) ? 0 : Mathf.Max(0, communicationSO.playerBBet - 1);
                communicationSO.GMToEnergySystem = true;
                ChangeColor();
            }
            round++;
            communicationSO.round = round;
            communicationSO.energySystemToGM = false;
        }
        if (communicationSO.tugSystemToGM)
        {
            communicationSO.result = communicationSO.tugResult;
            if (communicationSO.result != 0)
            {
                communicationSO.playerAReturn = (communicationSO.result > 0) ? 0 : Mathf.Max(0, communicationSO.playerABet - 1);
                communicationSO.playerBReturn = (communicationSO.result < 0) ? 0 : Mathf.Max(0, communicationSO.playerBBet - 1);
            }
            else
            {
                communicationSO.playerAReturn = communicationSO.playerABet;
                communicationSO.playerBReturn = communicationSO.playerBBet;
            }
            communicationSO.GMToEnergySystem = true;
            ChangeColor();
            communicationSO.tugSystemToGM = false;
        }
    }

    public void ReturnFromTugOfWar(int result)
    {
        communicationSO.result = result;
        if (communicationSO.result != 0)
        {
            communicationSO.playerAReturn = (communicationSO.result > 0) ? 0 : Mathf.Max(0, communicationSO.playerABet - 1);
            communicationSO.playerBReturn = (communicationSO.result < 0) ? 0 : Mathf.Max(0, communicationSO.playerBBet - 1);
        }
        else
        {
            communicationSO.playerAReturn = communicationSO.playerABet;
            communicationSO.playerBReturn = communicationSO.playerBBet;
        }
        communicationSO.GMToEnergySystem = true;
    }
}
