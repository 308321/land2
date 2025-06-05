using UnityEngine;

public class FinalImageController : MonoBehaviour
{
    public GameObject finalImage;  // 传入 FinalImage 对象
    public GameObject portal;  // 传入传送门对象

    private void Start()
    {
        if (finalImage != null && portal != null)
        {
            finalImage.SetActive(false);  // 确保开始时图片隐藏
            portal.SetActive(false);  // 确保开始时传送门隐藏
        }
        else
        {
            Debug.LogError("FinalImage or Portal not assigned in the inspector!");
        }
    }

    // 关闭图片并显示传送门
    public void ShowPortalAfterImageClose()
    {
        if (finalImage != null && portal != null)
        {
            finalImage.SetActive(false);  // 关闭图片
            portal.SetActive(true);  // 显示传送门
            Debug.Log("图片关闭，传送门出现");
        }
        else
        {
            Debug.LogError("FinalImage or Portal is not assigned!");
        }
    }

    // 触发传送门的进入事件
    public void EnterPortal()
    {
        // 在此可以添加传送门触发后切换场景的逻辑
        Debug.Log("玩家进入传送门，切换场景");
    }
}
