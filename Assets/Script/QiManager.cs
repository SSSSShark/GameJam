using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiManager : MonoBehaviour
{
    EnergySystem ES;
    public GameObject[] qi;
    [SerializeField] bool player;

    // Start is called before the first frame update
    void Start()
    {
        ES = FindObjectOfType<EnergySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            for (int i = 0; i < qi.Length; i++)
            {
                qi[i].SetActive(i < ES.playerAEnergy);
            }
        }
        else
        {
            for (int i = 0; i < qi.Length; i++)
            {
                qi[i].SetActive(i < ES.playerBEnergy);
            }
        }
    }
}
