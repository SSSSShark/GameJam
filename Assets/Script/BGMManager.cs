using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public enum MusicType
    {
        tugKeyDown,
        UIbutton,
        arrowHuman,
        arrowTrain,
        winTug,
        winGame
    }
    [SerializeField]
    GameObject soundEffectPrefab;
    [SerializeField]
    List<AudioClip> loopAudioClips = new List<AudioClip>();
    [SerializeField]
    List<AudioClip> audioClipPlayonce = new List<AudioClip>();
    //List
    AudioSource audioSource;
    public int playIndex;
    public static BGMManager me;
    void Awake() {
        me = this;
        audioSource = GetComponent<AudioSource>();
        playIndex = loopAudioClips.IndexOf(audioSource.clip);
    }
    public void SetLoopBGM(int index)
    {
        audioSource.clip = loopAudioClips[index];
        audioSource.Play();
        playIndex = index;
    }

    public void PlaySoundEffect(MusicType mt)
    {
        int index = (int)mt;
        GameObject se = Instantiate<GameObject>(soundEffectPrefab);
        AudioSource effectSource = se.GetComponent<AudioSource>();
        effectSource.clip = audioClipPlayonce[index];
        effectSource.Play();
        Destroy(se, effectSource.clip.length + 0.1f);
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

    [ContextMenu("player sound effect")]
    public void PlayText()
    {
        PlaySoundEffect(MusicType.winTug);
    }
}
