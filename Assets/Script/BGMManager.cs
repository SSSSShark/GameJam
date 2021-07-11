using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public enum MusicType
    {
        tugKeyDown,
        UIbutton,
        arrowLight1,
        arrowLight2,
        arrowLight3,
        arrowLight5,
        winGame,
        enterCrossPoint
    }

    [SerializeField]
    List<AudioClip> loopAudioClips = new List<AudioClip>();
    //List
    AudioSource audioSource;
    public int playIndex;
    void Awake() {
        audioSource = GetComponent<AudioSource>();
        playIndex = loopAudioClips.IndexOf(audioSource.clip);
    }
    public void SetLoopBGM(int index)
    {
        index %= loopAudioClips.Count;
        audioSource.clip = loopAudioClips[index];
        audioSource.Play();
        playIndex = index;
    }

    void Update() {
        if(Train.me == null) return;
        if(Train.me.communicationSO.gameEnd)
        {
            if(playIndex != 0)
            {
                SetLoopBGM(0);
            }

        }
        else if(playIndex != 1)
        {
            SetLoopBGM(1);
        }
    }
}
