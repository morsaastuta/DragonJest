using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public GameObject npc;

    public GameObject imageBox;
    public GameObject catHUD;
    public TextMeshProUGUI catText;
    public GameObject personHUD;
    public TextMeshProUGUI personText;
    public Image profile;
    public Sprite catSprite;
    public List<string> messages;
    public int currentMessage = -1;
    private NPCproperties NPCproperties;
    public bool npcTurn = false;
    public bool signal = false;

    public GameObject optionPane;

    public SceneController sceneController;

    bool interacting = false;

    void Update()
    {
        if (interacting && Input.GetKeyDown(KeyCode.Mouse0) && npcTurn)
        {
            Interact();
        }
    }

    public void NextCandidate(GameObject npc)
    {
        currentMessage = -1;

        NPCproperties = npc.GetComponent<NPCproperties>();
        profile.sprite = NPCproperties.face;

        imageBox.SetActive(true);
        catHUD.SetActive(true);
        profile.sprite = catSprite;
        catText.SetText(NPCproperties.presentation);

        interacting = true;
        messages = NPCproperties.messages;
        Interact();
    }

    public void Interact()
    {
        foreach (string message in messages)
        {
            if (currentMessage == 0)
            {
                catHUD.SetActive(false);
                personHUD.SetActive(true);
                profile.sprite = NPCproperties.face;
                sceneController.CameraTransition(true);
            }
            if (currentMessage > messages.Capacity - 1)
            {
                EndInteraction();
            }
            else if (currentMessage == messages.IndexOf(message)) personText.SetText(message);
            if (currentMessage == messages.Capacity - 1)
            {
                optionPane.SetActive(true);
                npcTurn = false;
            }
        }
        currentMessage++;
        
        void EndInteraction()
        {
            interacting = false;
            personHUD.SetActive(false);
            signal = true;
            npcTurn = false;
        }
    }
}
