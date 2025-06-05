using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool taskCompleted = false; // 任务完成标志

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CompleteTask()
    {
        taskCompleted = true;
        Debug.Log("任务完成！");
    }
}
