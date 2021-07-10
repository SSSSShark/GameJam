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
        EditorSceneManager.LoadScene(1);
    }

    public void CloseIntro()
    {
        intro.SetActive(false);
    }

    public void CloseStuff()
    {
        stuff.SetActive(false);
    }

    public void OpenIntro()
    {
        intro.SetActive(true);
    }

    public void OpeneStuff()
    {
        stuff.SetActive(true);
    }
}
