using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("最大回合数")]
    [SerializeField] private int roundNum = 20;
    [SerializeField] Communitcation communicationSO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    //通知拔河系统
                }
            }
            else
            {
                communicationSO.result = 0;
                communicationSO.playerAReturn = (communicationSO.result > 0) ? 0 : Mathf.Max(0, communicationSO.playerABet - 1);
                communicationSO.playerBReturn = (communicationSO.result < 0) ? 0 : Mathf.Max(0, communicationSO.playerBBet - 1);
                communicationSO.GMToEnergySystem = true;
            }
        }
        communicationSO.energySystemToGM = false;
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
