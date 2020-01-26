using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BehaviorUi : MonoBehaviour
{
    public string startScene;
    private VideoPlayer credits;
    public GameObject rawimage;
    public GameObject button_stop;

    public void StartMenuFunc()
    {
        Debug.Log(SceneManager.sceneCount);
        SceneManager.LoadScene(startScene);
        SceneManager.LoadScene(SceneManager.GetSceneByName(startScene).buildIndex);
    }

    public void PlayVideo()
    {
        Debug.Log("Play");
        credits=GetComponent<VideoPlayer>();
        rawimage.SetActive(true);
        button_stop.SetActive(true);
        credits.Play();
    }

    public void EndVideo()
    {
        credits = GetComponent<VideoPlayer>();
        rawimage.SetActive(false);
        credits.Stop();
        button_stop.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
