using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteanimator : MonoBehaviour
{
    private Animator anim;

    private float timer;

    public float min;
    public float max;
   
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            anim.SetTrigger("blink");
            timer = Random.Range(min, max);
        }
    }
}
