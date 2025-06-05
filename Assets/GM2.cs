using UnityEngine;

public class GM2 : MonoBehaviour
{
    public static GM2 Instance;

    public bool taskCompleted = false;  // 沈括篇章任务完成标志
    public int currentChapter = 2;  // 这个只会用于沈括篇章，值为 2

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 确保 GM2 不会在场景切换时销毁
        }
        else
        {
            Destroy(gameObject);  // 防止场景中多个 GM2
        }
    }

    // 其他沈括篇章的管理逻辑...
    public bool stoneTaskCompleted = false;

    public void MarkStoneTaskCompleted()
    {
        stoneTaskCompleted = true;
        Debug.Log("小石子任务完成！");
    }

}
