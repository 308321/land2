using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PicturePopup : MonoBehaviour
{
    [Header("弹窗 UI")]
    public GameObject popupPanel;               // 控制显示/隐藏的 Panel
    public Image popupImage;                    // 显示大图的 Image
    public TextMeshProUGUI descriptionText;     // 显示描述文字的 Text

    [Header("当前图片与描述")]
    public Sprite largeSprite;                  // 当前点击后显示的大图
    [TextArea]
    public string description;                  // 当前点击后显示的文字

    private void Update()
    {
        // 左键点击
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    Debug.Log("你点到了我：" + gameObject.name);

                    // 确保引用都存在
                    if (popupPanel != null && popupImage != null && descriptionText != null)
                    {
                        // 切换显隐
                        bool isActive = !popupPanel.activeSelf;
                        popupPanel.SetActive(isActive);

                        if (isActive)
                        {
                            popupImage.sprite = largeSprite;
                            descriptionText.text = description;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("❗有 UI 元素引用为空，请检查 Inspector 设置！");
                    }
                }
            }
        }
    }
}
