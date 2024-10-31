using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using static Unity.VisualScripting.Member;

public class SceneController : MonoBehaviour
{
    public DialogueController dialogueController;
    public List<GameObject> NPCs;
    public int currentNPC;
    public bool enterCandidate;
    public bool exitCandidate;
    private int cooldown;
    private int cooldownMax = 165;
    private Animator animator;

    public GameObject blackout;
    public TextMeshProUGUI introutro;

    public CameraTrasposition cameraController;

    public GameObject botitas;
    public AudioClip botitasIntro;

    public GameObject faceHUD;

    public bool choosing = false;
    public bool hasEaten = false;

    private AudioSource source;

    private string intro = "Estimiaudos habitantes de Craunía: \r\n\r\nNuestra Ignífuga Majestad se aburre. \r\n\r\nEs por ello que ha tenido la miauravillosa idea de convocaros (si es que acaso tenéis sentido del humor) al salón del trono. Aquel que consiga hacer reír a Nuestro Llameante Soberano, ganando así su simpatía, será considerado heredero del reino y, por tanto, legítimo dueño de la Corona.\r\nLas puertas se abrirán al amiaunecer, pero no aseguro que haya segundas oportunidades. Yo ya lo he intentado y no tuve miaucho éxito: Van dos gatos y no se cae ninguno porque los gatos siempre caen de pie jeje.\r\n\r\n\tAtentamente: Botitas, Visir de su Inflamable y Escamado Monarca. \r\n";

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = botitasIntro;
        source.Play();
        introutro.SetText(intro);
        currentNPC = -1;
        cooldown = cooldownMax;
    }

    void Update()
    {
        Debug.Log(currentNPC + " " + NPCs.Capacity);
        if (!choosing)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !enterCandidate && !exitCandidate) StartCandidate();
            if (enterCandidate && cooldown > 0) EnterCandidate(NPCs[currentNPC]);
            if (exitCandidate) ExitCandidate(NPCs[currentNPC]);
        }
    }

    public void StartCandidate()
    {
        source.Stop();
        blackout.SetActive(false);
        faceHUD.SetActive(true);
        if (hasEaten)
        {
            NPCs.Remove(NPCs[currentNPC]);
            currentNPC--;
            hasEaten = false;
            NPCs.TrimExcess();
        }
        currentNPC++;
        dialogueController.NextCandidate(NPCs[currentNPC]);
        enterCandidate = true;
        exitCandidate = false;
        dialogueController.signal = false;
        animator = NPCs[currentNPC].GetComponent<Animator>();
        animator.SetTrigger("Move");
    }
    
    public void EnterCandidate(GameObject npc)
    {
        npc.transform.position += new Vector3(-.25f, 0, 0);
        cooldown--;
        if (cooldown <= 0)
        {
            animator.SetTrigger("Stand");
            dialogueController.npcTurn = true;
            cooldown = cooldownMax;
            choosing = true;
            enterCandidate = false;
        }
    }

    public void ExitCandidate(GameObject npc)
    {
        if (dialogueController.signal)
        {
            npc.transform.position += new Vector3(.25f, 0, 0);
            cooldown--;
            if (cooldown <= 0)
            {
                cooldown = cooldownMax;
                StartCandidate();
            }
        }
    }

    public void CameraTransition(bool threeOrTwo)
    {
        animator.SetTrigger("Perspective");
        switch (threeOrTwo)
        {
            case true:
                cameraController.SideCam();
                NPCs[currentNPC].transform.rotation = Quaternion.Euler(0, 130, 0);
                botitas.transform.rotation = Quaternion.Euler(0, 130, 0);
                break;

            case false:
                cameraController.FrontCam();
                faceHUD.SetActive(false);
                NPCs[currentNPC].transform.rotation = Quaternion.Euler(0, 90, 0);
                botitas.transform.rotation = Quaternion.Euler(0, 90, 0);
                animator.SetTrigger("Move");
                break;
        }
    }

    public void Ending(string text)
    {
        blackout.SetActive(true);
        introutro.SetText(text);
    }
}