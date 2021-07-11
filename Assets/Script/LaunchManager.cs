using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class LaunchManager : MonoBehaviour
{
    public GameObject intro;
    public GameObject stuff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeganGame()
    {
        BGMManager.me.PlaySoundEffect(BGMManager.MusicType.UIbutton);
        EditorSceneManager.LoadScene(1);
    }

    public void CloseIntro()
    {
        BGMManager.me.PlaySoundEffect(BGMManager.MusicType.UIbutton);
        intro.SetActive(false);
    }

    public void CloseStuff()
    {
        BGMManager.me.PlaySoundEffect(BGMManager.MusicType.UIbutton);
        stuff.SetActive(false);
    }

    public void OpenIntro()
    {
        BGMManager.me.PlaySoundEffect(BGMManager.MusicType.UIbutton);
        intro.SetActive(true);
    }

    public void OpeneStuff()
    {
        BGMManager.me.PlaySoundEffect(BGMManager.MusicType.UIbutton);
        stuff.SetActive(true);
    }
}
