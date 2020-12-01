using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIanim : MonoBehaviour
{
    public Image I;


    [SerializeField]
    private Sprite[] anim;

    private int step;

    [SerializeField]
    private float fps = 30f;

    private float fpscounter;

    private void Awake()
    {
        I = GetComponent<Image>();
    }

    private void Update()
    {
        fpscounter += Time.deltaTime;

        I.sprite = anim[step];

        if (fpscounter >= 1f / fps)
        {
            if (step == (anim.Length - 1))
            {
                step = 0;
            }
            else step++;

            fpscounter = 0;
        }
    }
}
