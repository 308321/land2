using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;


public class Portal2 : MonoBehaviour
{
    public string sceneToLoad = "EndCutscene";  // 要切换的目标场景
    public VideoPlayer videoPlayer;         // 拖入VideoPlayer组件
    public CanvasGroup fadeCanvas;          // 可选：黑屏淡入淡出

    private bool hasEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasEntered && other.CompareTag("Player"))
        {
            hasEntered = true; // 防止多次触发
            StartCoroutine(PlayVideoThenLoadScene());
        }
    }

    IEnumerator PlayVideoThenLoadScene()
    {
        // 可选：黑屏淡入
        if (fadeCanvas != null)
        {
            yield return StartCoroutine(FadeIn());
        }

        // 播放穿梭视频
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();

        yield return new WaitUntil(() => videoPlayer.isPlaying);
        yield return new WaitUntil(() => !videoPlayer.isPlaying);

        // 加载目标场景
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator FadeIn()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = t;
            yield return null;
        }
    }
}
