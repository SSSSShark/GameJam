using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public enum Team{
        A,
        B
    }
    public Team team;
    void OnTriggerEnter(Collider other) {
        if(team == Team.A)
        {
            GameManager gm = FindObjectOfType<GameManager>();
            gm.AWinGame();
        }
        else
        {
            GameManager gm = FindObjectOfType<GameManager>();
            gm.BWinGame();
        }
    }
}
