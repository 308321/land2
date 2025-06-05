using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // 引用场景管理
using TMPro; // 引用 TextMeshPro
using UnityEngine.UI; // 引用 UI

public class DM0 : MonoBehaviour
{
    public GameObject dialoguePanel;           // 对话框 Panel
    public TextMeshProUGUI nameText;           // 显示人物名字的文本
    public TextMeshProUGUI dialogueText;       // 显示对话内容的文本

    [TextArea(2, 5)]
    public string[] dialogueLines;             // 对话内容
    public string[] characterNames;            // 人物名字

    private int currentLine = 0;
    private bool isDialogueActive = false;

    [Header("流光相关")]
    public GameObject lightEffectPrefab;       // 流光粒子预制体
    public Transform effectSpawnPoint;         // 流光生成位置（书的上方）
    public float lightExpandDuration = 3f;     // 流光扩散时间
    public string nextSceneName = "SampleScene"; // 下一场景的名字（此处是 "SampleScene"）

    // 新增部分：头像相关
    public Image playerPortrait;               // 玩家头像 UI Image
    public Sprite playerAvatar;                // 玩家头像

    void Start()
    {
        // 移除自动启动对话
        // StartDialogue(); 
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        currentLine = 0;
        isDialogueActive = true;
        ShowLine();
    }

    void Update()
    {
        // 按 F 键继续对话
        if (isDialogueActive && Input.GetKeyDown(KeyCode.F))
        {
            currentLine++;
            if (currentLine < dialogueLines.Length)
            {
                ShowLine();
            }
            else
            {
                EndDialogue(); // 对话结束
            }
        }
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

        // 开始播放流光特效并切换场景
        StartCoroutine(PlayLightEffectAndTransition());
    }

    IEnumerator PlayLightEffectAndTransition()
    {
        // 1. 生成粒子特效
        GameObject effect = Instantiate(lightEffectPrefab, effectSpawnPoint.position, Quaternion.identity);

        // 2. 获取粒子系统并播放
        ParticleSystem particleSystem = effect.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            // 启动粒子播放
            particleSystem.Play();
        }
        else
        {
            Debug.LogError("No ParticleSystem found on the instantiated effect!");
        }

        // 3. 扩散动画
        float timer = 0f;
        Vector3 startScale = effect.transform.localScale;
        Vector3 targetScale = startScale * 10f;

        // 让流光扩散的效果
        while (timer < lightExpandDuration)
        {
            effect.transform.localScale = Vector3.Lerp(startScale, targetScale, timer / lightExpandDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        // 4. 切换场景
        SceneManager.LoadScene(nextSceneName);
    }
}
