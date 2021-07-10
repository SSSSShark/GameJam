using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public int playerAEnergy;
    public int playerBEnergy;
    [Header("��ʼ����")]
    public int initEnergy = 5;
    [Header("�������")]
    public int maxEnergy = 5;
    [Header("���A������ʾ")]
    public GameObject playerAKeyHint;
    [Header("���B������ʾ")]
    public GameObject playerBKeyHint;
    private bool startFlag;
    [SerializeField] private int playerAStatus;
    [SerializeField] private int playerBStatus;
    private bool[] availableDirect;//�ĸ�����ֵ��0�ϣ�1�£�2��3��
    [Header("ƴ��ʱ��")]
    [SerializeField] private float time = 2f;
    private float timeAccu;

    [SerializeField] private Communication communitcationSO;

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
        timeAccu = 0f;
        availableDirect = dirct;
        if (!playerAKeyHint.activeSelf)
        {
            playerAKeyHint.SetActive(true);
            playerBKeyHint.SetActive(true);
            communitcationSO.playerABet = communitcationSO.playerBBet = 0;
            communitcationSO.playerADirect = communitcationSO.playerBDirect = 4;
        }
    }

    public void StartEnergyCompeteTest()
    {
        startFlag = true;
        timeAccu = 0f;
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
    }

    public void EndEnergyCompete()
    {
        startFlag = false;
        communitcationSO.energySystemToGM = true;
        playerAEnergy -= communitcationSO.playerABet;
        playerBEnergy -= communitcationSO.playerBBet;
    }

    void ListenKey(KeyCode k,int direct,bool player)//player=trueΪA������ΪB
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
        availableDirect = new bool[4];
        playerAStatus = playerBStatus = 0;
        communitcationSO.energySystemToGM = false;
        playerAKeyHint.SetActive(false);
        playerBKeyHint.SetActive(false);
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
            timeAccu += Time.deltaTime;
            {
                if (timeAccu >= time)
                {
                    EndEnergyCompete();
                }
            }
        }
        else if (communitcationSO.GMToEnergySystem)
        {
            if (communitcationSO.result > 0)
            {
                if (playerAStatus < 0)
                    playerAStatus = 0;
                playerAStatus++;
                if (playerBStatus > 0)
                    playerBStatus = 0;
                if(!communitcationSO.playerBisBet)
                    playerBStatus--;
            }
            else if (communitcationSO.result < 0)
            {
                if (playerBStatus < 0)
                    playerBStatus = 0;
                playerBStatus++;
                if (playerAStatus > 0)
                    playerAStatus = 0;
                if (!communitcationSO.playerAisBet)
                    playerAStatus--;
            }
            else
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

            }
            AddEnergy(EnergyRewardNum(true), true);
            AddEnergy(EnergyRewardNum(false), false);
            AddEnergy(communitcationSO.playerAReturn, true);
            AddEnergy(communitcationSO.playerBReturn, false);
            communitcationSO.GMToEnergySystem = false;
            playerAKeyHint.SetActive(false);
            playerBKeyHint.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        
    }
}
