using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrusterBlock : BlockBase
{

    private Playerscript player;
    public override IEnumerator UB()
    {
        yield return new WaitForSeconds(.02f);
        upblock = false;
        rightblock = false;
        leftblock = false;
        downblock = false;

        RaycastHit hit;
        LayerMask l = 1 << 9;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.7f, l))
        {
            
                blockrotation = rotation.zero;
                upblock = true;
            

        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 0.7f, l))
        {

                if (upblock != true)
                {
                    blockrotation = rotation.three;
                }

                rightblock = true;
            
        }


        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 0.7f, l))
        {

            
                if (upblock != true || rightblock != true)
                {
                    blockrotation = rotation.six;
                }

                downblock = true;
            
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 0.7f, l))
        {

                if (upblock != true || rightblock != true || downblock != true)
                {
                    blockrotation = rotation.nine;
                }

                leftblock = true;
            
        }

        SpriteObj.GetComponent<SpriteRenderer>().sprite = BlockSettings[1].Texture;
        BlockSet();
        yield return null;
    }
}
