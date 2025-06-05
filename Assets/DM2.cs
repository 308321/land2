using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI; // ← 新增

public class DM2 : MonoBehaviour
{
    public static DM2 Instance;

    public TMP_Text dialogueText;  // 用来显示对话的文本
    public GameObject dialoguePanel;  // 对话框面板
    public GameObject finalImage;  // 用来显示的 FinalImage 图像对象
    public GameObject portal;  // 传送门对象
    private bool isDialogueActive = false;  // 是否有对话在进行
    private int currentSentenceIndex = 0;  // 当前显示的句子索引
    private List<string> dialogueSentences = new List<string>();  // 存储对话句子

    // 新增部分：头像显示相关
    public Image characterPortrait; // 头像 UI Image
    public List<Sprite> portraitSprites = new List<Sprite>(); // 存储对应每个对话的头像

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dialoguePanel.SetActive(false);  // 默认隐藏对话框
        finalImage.SetActive(false);  // 默认隐藏 FinalImage
        portal.SetActive(false);  // 默认隐藏传送门
    }

    public void ShowDialogueById(int id)
    {
        dialoguePanel.SetActive(true);  // 显示对话框
        isDialogueActive = true;  // 设置对话框激活状态
        currentSentenceIndex = 0;  // 重置对话的句子索引

        dialogueSentences.Clear();  // 清空之前的对话内容
        portraitSprites.Clear();  // 清空头像列表

        switch (id)
        {
            case 0:
                dialogueSentences.Add("我：我这是……又穿越了？");
                portraitSprites.Add(playerPortrait); // 对应头像

                dialogueSentences.Add("我：此处依山傍水，倒像是江南一隅。院门半掩，我不妨进去看看，打听打听。");
                portraitSprites.Add(playerPortrait);

                break;
            case 1:
                dialogueSentences.Add("沈括：咦？何人闯入？此处乃寒舍，莫非迷路至此？");
                portraitSprites.Add(shenPortrait); // 对应头像

                dialogueSentences.Add("我：小子无意冒犯。误入斯地，见庭中清幽，复闻水声，心生好奇，遂擅自入内，尚请先生恕罪。");
                portraitSprites.Add(playerPortrait);

                dialogueSentences.Add("沈括（朗声笑）：哈哈，少年好奇心盛，乃学问之基，岂可责怪？");
                portraitSprites.Add(shenPortrait);

                dialogueSentences.Add("沈括：吾正观水中物之浮沉，思索其中之理。汝既至此，可曾思，何以物有浮沉之别？");
                portraitSprites.Add(shenPortrait);

                dialogueSentences.Add("我：小子曾闻：物轻则浮，重则沉。不知是否为真？");
                portraitSprites.Add(playerPortrait);

                dialogueSentences.Add("沈括：诚哉斯言！然则，浮沉之机，不唯轻重可判。尚有妙理，藏于其内。");
                portraitSprites.Add(shenPortrait);

                dialogueSentences.Add("沈括：吾乃沈括，现寓居此地，闲暇之余，整理天下舆图，兼究世间诸理。汝若不弃，愿共探此道乎？");
                portraitSprites.Add(shenPortrait);

                dialogueSentences.Add("我（拱手）：愿从先生左右，习学此术。");
                portraitSprites.Add(playerPortrait);

                dialogueSentences.Add("沈括：善哉。自今而后，汝为吾助手。且先取物，轻置水面，观其所变。");
                portraitSprites.Add(shenPortrait);

                break;
            case 2:
                dialogueSentences.Add("沈括：重石沉底，筒舟泛水。盖物之沉浮，非独重轻所致，亦与其形制、受水之势相参也。此即所谓‘浮力’也。");
                portraitSprites.Add(shenPortrait);

                dialogueSentences.Add("我：由此可见，若质轻而体积大，则水推之力足以托之而浮。若质重而体小，则虽轻，亦沉。");
                portraitSprites.Add(playerPortrait);

                dialogueSentences.Add("沈括：善哉，孺子可教！");
                portraitSprites.Add(shenPortrait);

                break;
            default:
                dialogueSentences.Add("未知的对话内容。");
                portraitSprites.Add(defaultPortrait); // 默认头像
                break;
        }

        ShowNextSentence();  // 显示第一句对话
    }

    public void ShowNextSentence()
    {
        if (currentSentenceIndex < dialogueSentences.Count)
        {
            dialogueText.text = dialogueSentences[currentSentenceIndex];

            // 设置头像
            if (currentSentenceIndex < portraitSprites.Count && characterPortrait != null)
                characterPortrait.sprite = portraitSprites[currentSentenceIndex];

            currentSentenceIndex++;
        }
        else
        {
            CloseDialogue();  // 所有句子显示完毕，关闭对话框
        }
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;

        // 判断是否需要弹出 FinalImage
        if (dialogueSentences.Count > 0 && dialogueSentences[dialogueSentences.Count - 1].Contains("孺子可教"))
        {
            ShowFinalImage();  // 如果是最后一段对话，显示 FinalImage
        }
    }

    private void ShowFinalImage()
    {
        finalImage.SetActive(true);  // 显示 FinalImage
        portal.SetActive(true);  // 显示传送门
        Debug.Log("Final Image and Portal shown!");
    }

    public bool IsDialogueActive
    {
        get { return isDialogueActive; }
    }

    public void StartDialogue(int dialogueId)
    {
        ShowDialogueById(dialogueId);
    }

    // 新增：角色头像引用
    public Sprite playerPortrait;
    public Sprite shenPortrait;
    public Sprite defaultPortrait;
}


/*
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DM2 : MonoBehaviour
{
    public static DM2 Instance;

    public TMP_Text dialogueText;  // 用来显示对话的文本
    public GameObject dialoguePanel;  // 对话框面板
    public GameObject finalImage;  // 用来显示的 FinalImage 图像对象
    public GameObject portal;  // 传送门对象
    private bool isDialogueActive = false;  // 是否有对话在进行
    private int currentSentenceIndex = 0;  // 当前显示的句子索引
    private List<string> dialogueSentences = new List<string>();  // 存储对话句子

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dialoguePanel.SetActive(false);  // 默认隐藏对话框
        finalImage.SetActive(false);  // 默认隐藏 FinalImage
        portal.SetActive(false);  // 默认隐藏传送门
    }

    public void ShowDialogueById(int id)
    {
        dialoguePanel.SetActive(true);  // 显示对话框
        isDialogueActive = true;  // 设置对话框激活状态
        currentSentenceIndex = 0;  // 重置对话的句子索引

        dialogueSentences.Clear();  // 清空之前的对话内容
        switch (id)
        {
            case 0:
                dialogueSentences.Add("我：我这是……又穿越了？");
                dialogueSentences.Add("我：此处依山傍水，倒像是江南一隅。院门半掩，我不妨进去看看，打听打听。");
                break;
            case 1:
                dialogueSentences.Add("沈括：咦？何人闯入？此处乃寒舍，莫非迷路至此？");
                dialogueSentences.Add("我：小子无意冒犯。误入斯地，见庭中清幽，复闻水声，心生好奇，遂擅自入内，尚请先生恕罪。");
                dialogueSentences.Add("沈括（朗声笑）：哈哈，少年好奇心盛，乃学问之基，岂可责怪？");
                dialogueSentences.Add("沈括：吾正观水中物之浮沉，思索其中之理。汝既至此，可曾思，何以物有浮沉之别？");
                dialogueSentences.Add("我：小子曾闻：物轻则浮，重则沉。不知是否为真？");
                dialogueSentences.Add("沈括：诚哉斯言！然则，浮沉之机，不唯轻重可判。尚有妙理，藏于其内。");
                dialogueSentences.Add("沈括：吾乃沈括，现寓居此地，闲暇之余，整理天下舆图，兼究世间诸理。汝若不弃，愿共探此道乎？");
                dialogueSentences.Add("我（拱手）：愿从先生左右，习学此术。");
                dialogueSentences.Add("沈括：善哉。自今而后，汝为吾助手。且先取物，轻置水面，观其所变。");
                break;
            case 2:
                dialogueSentences.Add("沈括：重石沉底，筒舟泛水。盖物之沉浮，非独重轻所致，亦与其形制、受水之势相参也。此即所谓‘浮力’也。");
                dialogueSentences.Add("我：由此可见，若质轻而体积大，则水推之力足以托之而浮。若质重而体小，则虽轻，亦沉。");
                dialogueSentences.Add("沈括：善哉，孺子可教！");
                break;
            default:
                dialogueSentences.Add("未知的对话内容。");
                break;
        }

        ShowNextSentence();  // 显示第一句对话
    }

    public void ShowNextSentence()
    {
        if (currentSentenceIndex < dialogueSentences.Count)
        {
            dialogueText.text = dialogueSentences[currentSentenceIndex];
            currentSentenceIndex++;
        }
        else
        {
            CloseDialogue();  // 所有句子显示完毕，关闭对话框
        }
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;

        // 判断是否需要弹出 FinalImage
        if (dialogueSentences.Count > 0 && dialogueSentences[dialogueSentences.Count - 1].Contains("孺子可教"))
        {
            ShowFinalImage();  // 如果是最后一段对话，显示 FinalImage
        }
    }

    private void ShowFinalImage()
    {
        finalImage.SetActive(true);  // 显示 FinalImage
        portal.SetActive(true);  // 显示传送门
        Debug.Log("Final Image and Portal shown!");
    }

    public bool IsDialogueActive
    {
        get { return isDialogueActive; }
    }

    public void StartDialogue(int dialogueId)
    {
        ShowDialogueById(dialogueId);
    }
}
*/