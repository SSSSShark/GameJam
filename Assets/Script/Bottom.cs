using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bottom : MonoBehaviour
{
    public Communication communicationSO;
    public Sprite up;
    public Sprite down;
    public bool player;
    public int stayMax;
    int stay=4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (communicationSO.isPressSpace)
            {
                gameObject.GetComponent<Image>().sprite = down;
                stay = stayMax;
            }
            else
            {
                stay--;
                if (stay <= 0)
                {
                    gameObject.GetComponent<Image>().sprite = up;
                }
            }

        }
        else
        {
            if (communicationSO.isPressDot)
            {
                gameObject.GetComponent<Image>().sprite = down;
                stay = stayMax;
            }
            else
            {
                stay--;
                if (stay <= 0)
                {
                    gameObject.GetComponent<Image>().sprite = up;
                }
            }
        }
    }
}
