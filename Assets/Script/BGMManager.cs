using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> audioClips = new List<AudioClip>();
    AudioSource audioSource;
    public int playIndex;
    void Awake() {
        audioSource = GetComponent<AudioSource>();
        playIndex = audioClips.IndexOf(audioSource.clip);
    }
    public void SetBGM(int index)
    {
        index %= audioClips.Count;
        audioSource.clip = audioClips[index];
        audioSource.Play();
        playIndex = index;
    }

    void Update() {
        if(Train.me == null) return;
        if(Train.me.communicationSO.gameEnd)
        {
            if(playIndex != 0)
            {
                SetBGM(0);
            }

        }
        else if(playIndex != 1)
        {
            SetBGM(1);
        }
    }
}
