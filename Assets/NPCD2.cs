using UnityEngine;

public class NPCD2 : MonoBehaviour
{
    public int dialogueId;
    public string npcName;
    public GameObject finalImage; // 引用 FinalImage 对象

    private bool isPlayerInRange = false;  // 玩家是否进入了 NPC 的触发区域

    private void Start()
    {
    }

    private void Update()
    {
        // 如果玩家进入触发区域并按下 F 键
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed!");

            if (DM2.Instance.IsDialogueActive)
            {
                DM2.Instance.ShowNextSentence(); // 显示下一句对话
            }
            else if (finalImage.activeSelf)  // 如果 FinalImage 显示中，按 F 键关闭它
            {
                finalImage.SetActive(false);
                Debug.Log("Final Image closed!");
            }
            else
            {
                Debug.Log("Triggering dialogue...");

                // 判断当前篇章，启动正确的对话
                if (GM2.Instance != null && GM2.Instance.currentChapter == 2)  // 沈括篇章
                {
                    if (GM2.Instance.stoneTaskCompleted)
                    {
                        DM2.Instance.StartDialogue(2); // 如果任务完成，启动case 2
                    }
                    else
                    {
                        DM2.Instance.StartDialogue(dialogueId); // 没完成就用原本的对话
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player entered NPC's trigger area!");  // 进入触发器时的日志提示
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player exited NPC's trigger area!");  // 离开触发器时的日志提示
        }
    }
}
