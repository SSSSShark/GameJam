using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] Communication communicationSO;
    [SerializeField] GameObject up;
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;
    [SerializeField] bool player;
    EnergySystem ES;

    // Start is called before the first frame update
    void Start()
    {
        ES = FindObjectOfType<EnergySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        up.SetActive(communicationSO.availableDirect[0]);
        left.SetActive(communicationSO.availableDirect[2]);
        right.SetActive(communicationSO.availableDirect[3]);
        if (ES.showFlag)
        {
            if (player && communicationSO.result > 0)
            {
                up.SetActive(communicationSO.playerADirect == 0);
                left.SetActive(communicationSO.playerADirect == 2);
                right.SetActive(communicationSO.playerADirect == 3);
            }
            else if (!player && communicationSO.result < 0)
            {
                up.SetActive(communicationSO.playerBDirect == 0);
                left.SetActive(communicationSO.playerBDirect == 2);
                right.SetActive(communicationSO.playerBDirect == 3);
            }
            else
            {
                up.SetActive(false);
                left.SetActive(false);
                right.SetActive(false);
            }
        }
    }
}
