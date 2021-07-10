using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public int playerAEnergy;
    public int playerBEnergy;
    [Header("初始气量")]
    public int initEnergy = 5;
    [Header("最大气量")]
    public int maxEnergy = 5;
    [Header("玩家A按键提示")]
    public GameObject playerAKeyHint;
    [Header("玩家B按键提示")]
    public GameObject playerBKeyHint;
    private bool startFlag;
    private bool showFlag;
    [SerializeField] private int playerAStatus;
    [SerializeField] private int playerBStatus;
    private bool[] availableDirect;//四个布尔值，0前，1下，2左，3右
    [Header("拼气时间")]
    [SerializeField] private float competeTime = 1.2f;
    private float competeTimeAcc;
    [SerializeField] private float showTime = 0.8f;
    private float showTimeAcc;

    [SerializeField] private Communication communitcationSO;
    [SerializeField] private Train train;

    public void AddEnergy(int n, bool player)
    {
        if (player)
        {
            playerAEnergy += n;
            if (playerAEnergy > maxEnergy)
            {
                playerAEnergy = maxEnergy;
            }
        }
        else
        {
            playerBEnergy += n;
            if (playerBEnergy > maxEnergy)
            {
                playerBEnergy = maxEnergy;
            }
        }
    }

    public void StartEnergyCompete(bool[] dirct)
    {
        startFlag = true;
        competeTimeAcc = 0f;
        availableDirect = dirct;
        if (!playerAKeyHint.activeSelf)
        {
            playerAKeyHint.SetActive(true);
            playerBKeyHint.SetActive(true);
            communitcationSO.playerABet = communitcationSO.playerBBet = 0;
            communitcationSO.playerADirect = communitcationSO.playerBDirect = 4;
        }
        communitcationSO.competeEnergy = true;
    }

    public void StartEnergyCompeteTest()
    {
        startFlag = true;
        competeTimeAcc = 0f;
        availableDirect[0]=true;
        availableDirect[1] = true;
        availableDirect[2] = true;
        availableDirect[3] = true;
        if (!playerAKeyHint.activeSelf)
        {
            playerAKeyHint.SetActive(true);
            playerBKeyHint.SetActive(true);
            communitcationSO.playerABet = communitcationSO.playerBBet = 0;
            communitcationSO.playerADirect = communitcationSO.playerBDirect = 4;
        }
        communitcationSO.competeEnergy = true;
    }

    public void EndEnergyCompete()
    {
        startFlag = false;
        communitcationSO.energySystemToGM = true;
        playerAEnergy -= communitcationSO.playerABet;
        playerBEnergy -= communitcationSO.playerBBet;
    }

    public void EndShowResult()
    {
        showFlag = false;
        communitcationSO.competeEnergy = false;
    }

    void ListenKey(KeyCode k,int direct,bool player)//player=true为A，否则为B
    {
        if (Input.GetKeyDown(k))
        {
            if (availableDirect[direct])
            {
                if (player)
                {
                    if (communitcationSO.playerADirect != direct)
                    {
                        communitcationSO.playerADirect = direct;
                        communitcationSO.playerABet = 1;
                    }
                    else if (communitcationSO.playerABet < 3 && communitcationSO.playerABet < playerAEnergy)
                    {
                        communitcationSO.playerABet += 1;
                    }
                }
                else
                {
                    if (communitcationSO.playerBDirect != direct)
                    {
                        communitcationSO.playerBDirect = direct;
                        communitcationSO.playerBBet = 1;
                    }
                    else if (communitcationSO.playerBBet < 3 && communitcationSO.playerBBet < playerBEnergy)
                    {
                        communitcationSO.playerBBet += 1;
                    }
                }
            }
        }
    }

    int EnergyRewardNum(bool player)
    {
        if (player)
        {
            if (playerAStatus == -1 || playerAStatus == 2)
                return 1;
            if (playerAStatus == -2 || playerAStatus == 3)
                return 2;
            if (playerAStatus <= -3 || playerAStatus >= 4)
                return 4;
        }
        else
        {
            if (playerBStatus == -1 || playerBStatus == 2)
                return 1;
            if (playerBStatus == -2 || playerBStatus == 3)
                return 2;
            if (playerBStatus <= -3 || playerBStatus >= 4)
                return 4;
        }
        return 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAEnergy = playerBEnergy = initEnergy;
        startFlag = false;
        showFlag = false;
        availableDirect = new bool[4];
        playerAStatus = playerBStatus = 0;
        communitcationSO.energySystemToGM = false;
        playerAKeyHint.SetActive(false);
        playerBKeyHint.SetActive(false);
        train = FindObjectOfType<Train>();
    }

    Train.Direction GetDir(int dir)
    {
        Train.Direction result;
        switch (dir)
        {
            case 0:
                result = Train.Direction.forward;
                break;
            case 1:
                result = Train.Direction.random;
                break;
            case 2:
                result = Train.Direction.left;
                break;
            case 3:
                result = Train.Direction.right;
                break;
            default:
                result = Train.Direction.random;
                break;
        }
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFlag)
        {
            ListenKey(KeyCode.W, 0, true);
            ListenKey(KeyCode.UpArrow, 0, false);
            ListenKey(KeyCode.S, 1, true);
            ListenKey(KeyCode.DownArrow, 1, false);
            ListenKey(KeyCode.A, 2, true);
            ListenKey(KeyCode.LeftArrow, 2, false);
            ListenKey(KeyCode.D, 3, true);
            ListenKey(KeyCode.RightArrow, 3, false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (communitcationSO.playerADirect >= 0 && communitcationSO.playerADirect <= 3 && playerAEnergy >= 5)
                {
                    communitcationSO.playerABet = 5;
                }
            }
            if (Input.GetKeyDown(KeyCode.KeypadPeriod))
            {
                if (communitcationSO.playerBDirect >= 0 && communitcationSO.playerBDirect <= 3 && playerBEnergy >= 5)
                {
                    communitcationSO.playerBBet = 5;
                }
            }
            competeTimeAcc += Time.deltaTime;
            {
                if (competeTimeAcc >= competeTime || communitcationSO.playerABet == 5 || communitcationSO.playerBBet == 5)
                {
                    EndEnergyCompete();
                }
            }
        }
        else if (communitcationSO.GMToEnergySystem)
        {
            if (communitcationSO.result > 0)//A赢
            {
                if (playerAStatus < 0)
                    playerAStatus = 0;
                playerAStatus++;
                if (playerBStatus > 0)
                    playerBStatus = 0;
                if(!communitcationSO.playerBisBet)
                    playerBStatus--;
                train.SetDir(GetDir(communitcationSO.playerADirect));
            }
            else if (communitcationSO.result < 0)//B赢
            {
                if (playerBStatus < 0)
                    playerBStatus = 0;
                playerBStatus++;
                if (playerAStatus > 0)
                    playerAStatus = 0;
                if (!communitcationSO.playerAisBet)
                    playerAStatus--;
                train.SetDir(GetDir(communitcationSO.playerBDirect));
            }
            else//平
            {
                if (communitcationSO.playerAisBet || communitcationSO.playerBisBet)
                {
                    if (playerAStatus > 0)
                        playerAStatus = 0;
                    if (playerBStatus > 0)
                        playerBStatus = 0;
                }
                else
                {
                    if (playerAStatus > 0)
                        playerAStatus = 0;
                    playerAStatus--;
                    if (playerBStatus > 0)
                        playerBStatus = 0;
                    playerBStatus--;
                }

                train.SetDir(Train.Direction.random);
            }
            AddEnergy(EnergyRewardNum(true), true);
            AddEnergy(EnergyRewardNum(false), false);
            AddEnergy(communitcationSO.playerAReturn, true);
            AddEnergy(communitcationSO.playerBReturn, false);
            communitcationSO.GMToEnergySystem = false;
            playerAKeyHint.SetActive(false);
            playerBKeyHint.SetActive(false);
            showFlag = true;
            showTimeAcc = 0;
        }
        else if (showFlag)
        {
            showTimeAcc += Time.deltaTime;
            if (showTimeAcc >= showTime)
            {
                EndShowResult();
            }
        }
    }

    private void FixedUpdate()
    {
        
    }
}
