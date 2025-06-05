using UnityEngine;

public class KeyPromptController : MonoBehaviour
{
    public GameObject keyPromptPanel;  // 将母级Panel拖到这里
    public GameObject keyPromptImage;  // 将提示图片拖到这里

    private void Start()
    {
        // 开场动画结束后，显示提示Panel
        keyPromptPanel.SetActive(true);
    }

    private void Update()
    {
        // 监听任意键按下，消失提示图片和Panel
        if (Input.anyKeyDown)
        {
            keyPromptPanel.SetActive(false);  // 隐藏整个Panel
            // 可以在这里触发进入第一个篇章的逻辑
        }
    }
}
