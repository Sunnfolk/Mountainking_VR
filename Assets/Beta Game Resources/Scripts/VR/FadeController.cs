using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{

    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetTrigger("FadeIn");
    }


    public void FadeIn ()
    {
        _anim.SetTrigger("FadeIn");
    }
    public void FadeOut ()
    {
        _anim.SetTrigger("FadeOut");
    }
}
