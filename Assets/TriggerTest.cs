using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "阴影部分")
        {
            Debug.Log("任务完成！");
            GameManager.Instance.taskCompleted = true;
            // 在任务完成时打印调试信息
            Debug.Log("任务已完成：" + GameManager.Instance.taskCompleted);

        }
    }
}
