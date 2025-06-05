using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI; // ← 新增

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TMP_Text dialogueText;
    public GameObject dialoguePanel;
    public GameObject finalImage;
    public GameObject portal;

    public Image characterPortrait; // ← 添加头像 UI Image
    public List<Sprite> portraitSprites = new List<Sprite>(); // ← 对应每句头像图片

    private bool isDialogueActive = false;
    private int currentSentenceIndex = 0;
    private List<string> dialogueSentences = new List<string>();

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

        dialoguePanel.SetActive(false);
        finalImage.SetActive(false);
        portal.SetActive(false);
    }

    public void ShowDialogueById(int id)
    {
        dialoguePanel.SetActive(true);
        isDialogueActive = true;
        currentSentenceIndex = 0;

        dialogueSentences.Clear();
        portraitSprites.Clear(); // ← 每次清空头像列表

        switch (id)
        {
            case 0:
                dialogueSentences.Add("我：哎呀，屁股好痛！这里是……什么地方？");
                portraitSprites.Add(playerPortrait); // ← 示例

                dialogueSentences.Add("我：那边亭子里好像有光，我去看看。");
                portraitSprites.Add(playerPortrait);
                break;

            case 1:
                dialogueSentences.Add("采莲：姑娘，我家小姐已在亭中候多时了。");
                portraitSprites.Add(servantPortrait);

                dialogueSentences.Add("我：此地何处？唤我前来所为何事？");
                portraitSprites.Add(playerPortrait);

                dialogueSentences.Add("采莲：姑娘怎的忘了？今日贞仪小姐约您共议‘月蚀’之理呢。快快请进。");
                portraitSprites.Add(servantPortrait);

                dialogueSentences.Add("（我：贞仪小姐……莫不是清代天文学家王贞仪？）");
                portraitSprites.Add(playerPortrait);
                break;

            case 2:
                dialogueSentences.Add("王贞仪：妹妹，我疑那‘天狗食月’之说，乃无稽之谈。若地如圆球，月食应是地影所蔽……奈何无人信我。");
                portraitSprites.Add(wangPortrait);

                dialogueSentences.Add("我：贞仪姐勿忧，今有我在，愿与姐姐共求其证。");
                portraitSprites.Add(playerPortrait);

                dialogueSentences.Add("王贞仪：如此甚好，我已备下灯笼与大小铜球，以拟日地月之象。且与我同布此阵，助我一臂之力。");
                portraitSprites.Add(wangPortrait);

                dialogueSentences.Add("我：姐姐发话，岂敢怠慢？包在我身上！");
                portraitSprites.Add(playerPortrait);
                break;

            case 3:
                dialogueSentences.Add("王贞仪：你看，这影子如弓！《周髀算经》所言‘地法覆槃’，谬矣！");
                portraitSprites.Add(wangPortrait);

                dialogueSentences.Add("我：妙极！此证昭然，贞仪姐之苦心，终得印证矣！");
                portraitSprites.Add(playerPortrait);
                break;

            default:
                dialogueSentences.Add("未知的对话内容。");
                portraitSprites.Add(defaultPortrait); // 默认头像
                break;
        }

        ShowNextSentence();
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
            CloseDialogue();
        }
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;

        if (dialogueSentences.Count > 0 && dialogueSentences[dialogueSentences.Count - 1].Contains("妙极！"))
        {
            ShowFinalImage();
        }
    }

    private void ShowFinalImage()
    {
        finalImage.SetActive(true);
        portal.SetActive(true);
        Debug.Log("Final Image and Portal shown!");
    }

    public bool IsDialogueActive => isDialogueActive;

    public void StartDialogue(int dialogueId)
    {
        ShowDialogueById(dialogueId);
    }

    // ← 添加角色头像引用（在 Inspector 中赋值）
    public Sprite playerPortrait;
    public Sprite servantPortrait;
    public Sprite wangPortrait;
    public Sprite defaultPortrait;
}
