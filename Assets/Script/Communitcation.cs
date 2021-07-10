using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObject/Communication",fileName ="CommunitcationSO")]
public class Communitcation : ScriptableObject
{
    public bool energySystemToGM;
    public int playerADirect;
    public int playerABet;
    public int playerBDirect;
    public int playerBBet;

    public bool GMToEnergySystem;
    public int result;
    public bool playerAisBet;
    public int playerAReturn;
    public bool playerBisBet;
    public int playerBReturn;

}
