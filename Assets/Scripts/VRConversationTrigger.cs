using UnityEngine;
using DialogueEditor;


public class VRConversationTrigger : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;

    public void StartConversation()
    {
        ConversationManager.Instance.StartConversation(myConversation);
    }
}
