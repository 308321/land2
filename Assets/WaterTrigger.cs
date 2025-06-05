using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered water!"); // 不管谁进来先打个Log

        if (other.CompareTag("Stone"))
        {
            Debug.Log("小石子入水了！");
            GM2.Instance.MarkStoneTaskCompleted();
        }
    }

}
