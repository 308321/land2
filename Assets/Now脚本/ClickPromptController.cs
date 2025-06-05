using UnityEngine;
using UnityEngine.EventSystems;

public class ClickPromptController : MonoBehaviour, IPointerClickHandler
{
    public GameObject clickPromptCanvas; // 将自己的 Canvas 拖进来
    public DM0 dialogueManager; // 拖 DialogueManager 对象进来

    public void OnPointerClick(PointerEventData eventData)
    {
        // 1. 隐藏点击提示
        clickPromptCanvas.SetActive(false);

        // 2. 开始对话
        dialogueManager.StartDialogue();
    }
}
