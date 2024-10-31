using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DragonController : MonoBehaviour
{
    public GameObject dragon;
    public AudioClip laughClip;
    public AudioClip ughClip;
    public AudioClip fireClip;
    public AudioClip crownLaughClip;

    public Animator animator;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        animator = dragon.GetComponent<Animator>();
    }

    public void DragonLaugh()
    {
        animator.SetTrigger("Laugh");
        source.clip = laughClip;
        source.Play();
    }

    public void DragonUgh()
    {
        animator.SetTrigger("Act");
        source.clip = ughClip;
        source.Play();
    }

    public void DragonFire()
    {
        animator.SetTrigger("Act");
        source.clip = fireClip;
        source.Play();
    }

    public void DragonCrown()
    {
        animator.SetTrigger("Laugh");
        source.clip = crownLaughClip;
        source.Play();
    }
}
