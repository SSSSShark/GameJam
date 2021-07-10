using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestText : MonoBehaviour
{
    [SerializeField] Communitcation communitcationSO;
    [SerializeField] bool player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = (player) ? communitcationSO.playerADirect.ToString() + communitcationSO.playerABet.ToString() 
            : communitcationSO.playerBDirect.ToString() + communitcationSO.playerBBet.ToString();
    }
}
