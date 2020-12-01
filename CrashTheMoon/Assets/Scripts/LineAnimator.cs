using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineAnimator : MonoBehaviour
{
    private LineRenderer LR;

    [SerializeField]
    private Texture[] anim;

    private int step;

    [SerializeField]
    private float fps = 30f;

    private float fpscounter;

    private void Awake()
    {
        LR = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        fpscounter += Time.deltaTime;

        LR.material.SetTexture("_MainTex", anim[step]);

        if(fpscounter >= 1f / fps)
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
