using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // 引用 UI

public class DM_Ending : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    [TextArea(2, 5)]
    public string[] dialogueLines;
    public string[] characterNames;

    private int currentLine = 0;
    private bool isDialogueActive = false;

    // 新增部分：头像相关
    public Image playerPortrait;               // 玩家头像 UI Image
    public Sprite playerAvatar;                // 玩家头像

    void Start()
    {
        StartCoroutine(AutoStartDialogue());
    }

    IEnumerator AutoStartDialogue()
    {
        yield return new WaitForSeconds(1f); // 给场景淡入时间，按需调整
        StartDialogue();
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.F))
        {
            currentLine++;
            if (currentLine < dialogueLines.Length)
            {
                ShowLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        currentLine = 0;
        isDialogueActive = true;
        ShowLine();
    }

    void ShowLine()
    {
        dialogueText.text = dialogueLines[currentLine];
        if (characterNames.Length > currentLine)
        {
            nameText.text = characterNames[currentLine];
        }

        // 每次更新对话时，更新头像
        if (playerPortrait != null && playerAvatar != null)
        {
            playerPortrait.sprite = playerAvatar;
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;

        // 可以在这里触发点击相框提示、Credits字幕等
    }
}
