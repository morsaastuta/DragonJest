using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class OptionController : MonoBehaviour
{
    public SceneController sceneController;
    public DialogueController dialogueController;
    public DragonController dragonController;

    public TextMeshProUGUI crownCounter;
    public TextMeshProUGUI deathCounter;

    public GameObject blackout;
    public TextMeshProUGUI introutro;

    private int deadCount = 0;
    public string deathEpilogue;
    public string warEpilogue;
    public string neutralEpilogue;

    public List<Status> statuses;

    public bool end;
    public SceneGoTo sceneGoTo;

    void Update()
    {
        crownCounter.SetText(sceneController.NPCs[sceneController.currentNPC].transform.parent.GetComponent<Status>().crowns.ToString());
        if (end && Input.GetKeyDown(KeyCode.Mouse0))
        {
            sceneGoTo.MainMenu();
        }
    }

    public void Laugh()
    {
        dragonController.DragonLaugh();
        // anim
        if (!checkCrowns())
        {
            sceneController.exitCandidate = true;
            sceneController.choosing = false;
            dialogueController.Interact();
            sceneController.CameraTransition(false);
        }
    }

    public void Bah()
    {
        dragonController.DragonUgh();
        // anim
        if (!checkCrowns())
        {
            sceneController.exitCandidate = true;
            sceneController.choosing = false;
            dialogueController.Interact();
            sceneController.CameraTransition(false);
        }
    }

    public void Eat()
    {
        dragonController.DragonFire();
        // anim
        GameObject checkNPC = sceneController.NPCs[sceneController.currentNPC];
        sceneController.NPCs.RemoveAll((npc) => eatPerson(checkNPC, npc));
        sceneController.NPCs[sceneController.currentNPC].SetActive(false);
        deadCount++;
        deathCounter.SetText(deadCount.ToString());
        sceneController.hasEaten = true;
        if (deadCount < 14)
        {
            if (!checkCrowns())
            {
                sceneController.exitCandidate = true;
                sceneController.choosing = false;
                dialogueController.Interact();
                sceneController.CameraTransition(false);
            }
        } else deathEnding();
    }

    public void Crown()
    {
        dragonController.DragonCrown();
        // anim
        sceneController.NPCs[sceneController.currentNPC].GetComponentInParent<Status>().crowns++;
        if(sceneController.NPCs[sceneController.currentNPC].GetComponentInParent<Status>().crowns != 3)
        {
            if (!checkCrowns())
            {
                sceneController.exitCandidate = true;
                sceneController.choosing = false;
                dialogueController.Interact();
                sceneController.CameraTransition(false);
            }
        } else crownEnding(sceneController.NPCs[sceneController.currentNPC].GetComponentInParent<Status>().ending);
    }

    private bool eatPerson(GameObject og, GameObject sibling)
    {
        if (og.transform.parent == sibling.transform.parent && og.transform != sibling.transform && sceneController.NPCs.IndexOf(sibling) > sceneController.NPCs.IndexOf(og))
        {
            Debug.Log("deleted " + sibling.name + " with parent " + sibling.transform.parent.name);
            sibling.SetActive(false);
            return true;
        }
        return false;
    }

    private void crownEnding(string epilogue)
    {
        blackout.SetActive(true);
        introutro.SetText(epilogue);
        end = true;
    }

    private void deathEnding()
    {
        blackout.SetActive(true);
        introutro.SetText(deathEpilogue);
        end = true;
    }

    private void warEnding()
    {
        blackout.SetActive(true);
        introutro.SetText(warEpilogue);
        end = true;
    }

    private void neutralEnding()
    {
        blackout.SetActive(true);
        introutro.SetText(neutralEpilogue);
        end = true;
    }

    private bool checkCrowns()
    {
        if (sceneController.currentNPC == sceneController.NPCs.Capacity - 1)
        {
            Debug.Log("enter");
            int maxCrowns = 0;
            bool repeat = false;

            for (int i = 0; i < statuses.Capacity; i++)
            {
                Debug.Log("init check");
                if (statuses[i].crowns == 2 && !(maxCrowns > 2))
                {
                    Debug.Log("existe bi-coronado");
                    if (maxCrowns != 2)
                    {
                        maxCrowns = 2;
                        repeat = false;

                    }
                    else repeat = true;
                }
                else if (statuses[i].crowns == 1 && !(maxCrowns > 1))
                {
                    Debug.Log("existe mono-coronado");
                    if (maxCrowns != 1)
                    {
                        maxCrowns = 1;
                        repeat = false;
                    }
                    else repeat = true;
                }
                Debug.Log("repeticion: " + repeat);
            }
            if (maxCrowns == 0) neutralEnding();
            else if (repeat) warEnding();
            else
            {
                string ending = "";
                int localMaxCrowns = 0;
                for (int i = 0; i < statuses.Capacity; i++)
                {
                    if (statuses[i].crowns > localMaxCrowns)
                    {
                        localMaxCrowns = statuses[i].crowns;
                        ending = statuses[i].ending;
                    }
                }
                crownEnding(ending);
            }
            return true;
        }
        else return false;
    }
}
