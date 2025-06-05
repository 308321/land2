using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public int dialogueId;
    public string npcName;
    public GameObject finalImage; // 引用 FinalImage 对象
    public Animator npcAnimator; // 引用 NPC 的 Animator 组件

    private bool isPlayerInRange = false;  // 玩家是否进入了 NPC 的触发区域

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (DialogueManager.Instance.IsDialogueActive)
            {
                DialogueManager.Instance.ShowNextSentence(); // 显示下一句对话
            }
            else if (finalImage.activeSelf)  // 如果 FinalImage 显示中，按 F 键关闭它
            {
                finalImage.SetActive(false);
                Debug.Log("Final Image closed!");
            }
            else
            {
                Debug.Log("F key pressed! Triggering dialogue...");
                if (GameManager.Instance.taskCompleted && dialogueId == 2)
                {
                    // 如果任务完成且当前对话是case 2，切换为case 3
                    dialogueId = 3;
                    Debug.Log("任务完成，切换到case 3!");
                }

                // 触发动画播放
                PlayBowAnimation();

                // 启动对话
                DialogueManager.Instance.StartDialogue(dialogueId);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    // 播放侍女作揖的动画
    private void PlayBowAnimation()
    {
        if (npcAnimator != null)
        {
            npcAnimator.SetTrigger("PlayBow");  // 假设你在 Animator 中创建了一个名为 "PlayBow" 的 Trigger
            Debug.Log("Playing Bow Animation");
        }
        else
        {
            Debug.LogWarning("NPC Animator is not assigned!");
        }
    }
}
