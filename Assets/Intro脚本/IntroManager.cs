using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video; // 添加这个引用来使用 VideoPlayer  
using UnityEngine.SceneManagement; // 添加这个命名空间 

public class IntroManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private string videoPath = "video/开场动画.mp4  ";
    // Start is called before the first frame update
    void Start()
    {
        string fullPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoPath);
        videoPlayer.url = fullPath;
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.Play();
    }

    // Update is called once per frame
    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("Now");
    }
}
