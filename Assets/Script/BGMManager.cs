using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    List<AudioClip> audioClips = new List<AudioClip>();
    AudioSource audioSource;
    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    public void SetBGM(int index)
    {
        index %= audioClips.Count;
        audioSource.clip = audioClips[index];
    }
}
