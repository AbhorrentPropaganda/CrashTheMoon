using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldBlock : BlockBase
{
    public GameObject ShieldComponent;
    public float timetorecharge;
    private float rechargetime;
    public GameObject currentshield;
    private bool anim;
    private bool anim2;
    private Animator animator;

    public override void UpdateBlock()
    {
        
    }

    public void Awake()
    {
        animator =  GetComponentInChildren<Animator>();
    }

    public override void Update()
    {
        if (currentshield == null && !anim)
        {
           
            animator.SetBool("depleted", true);
            anim = true;
        }
        else animator.SetBool("depleted", false);

        if (currentshield != null && !anim2)
        {

            animator.SetBool("go", true);
            anim2 = true;
        }
        else animator.SetBool("go", false);


        if (currentshield == null&&placed)
        {
            rechargetime -= Time.deltaTime;
            if(rechargetime <= 0)
            {
                currentshield = Instantiate(ShieldComponent, transform.position, new Quaternion(90,0,0,0), transform.parent);
                
                
                rechargetime = timetorecharge;
            }
            else animator.SetBool("go", false);
        }
    }

    
}
