using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public int npcId;  // NPC对应的ID

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 调用DialogueManager直接触发对话
            DialogueManager.Instance.ShowDialogueById(npcId);
        }
    }
}
