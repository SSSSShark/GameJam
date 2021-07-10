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

    // Start is called before the first frame update
    void Start()
    {
        round = 0;
        train = FindObjectOfType<Train>();
        AWin.SetActive(false);
        BWin.SetActive(false);
        communicationSO.gameEnd = false;
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
            }
            round++;
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
