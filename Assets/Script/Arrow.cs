using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    public int direction;
    public bool player;
    public float denominator;
    [SerializeField] Communication communicationSO;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = communicationSO.arrow[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (communicationSO.playerADirect != direction)
            {
                gameObject.GetComponent<Image>().sprite = communicationSO.arrow[0];
                gameObject.GetComponent<Image>().transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = communicationSO.arrow[Mathf.Min(communicationSO.playerABet, 4)];
                gameObject.GetComponent<Image>().transform.localScale = new Vector3(1f + ((float)communicationSO.playerABet / denominator), 1f + ((float)communicationSO.playerABet / denominator), 1f);
            }
        }
        else
        {
            if (communicationSO.playerBDirect != direction)
            {
                gameObject.GetComponent<Image>().sprite = communicationSO.arrow[0];
                gameObject.GetComponent<Image>().transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = communicationSO.arrow[Mathf.Min(communicationSO.playerBBet, 4)];
                gameObject.GetComponent<Image>().transform.localScale = new Vector3(1f + ((float)communicationSO.playerBBet / denominator), 1f + ((float)communicationSO.playerBBet / denominator), 1f);
            }
        }
    }
}
