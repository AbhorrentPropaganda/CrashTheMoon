using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockBase : MonoBehaviour
{
    public bool CanUpdate;
    public bool placed;

    public enum rotation { zero, three, six, nine}
    public rotation blockrotation;

    public enum blocktype { no, one, twodiag, twostraight, three, four}
    public blocktype block;

    public bool upblock;
    public bool rightblock;
    public bool downblock;
    public bool leftblock;
    public bool blockspace;
    private bool canupdate;

    public GameObject SpriteObj;

    private int connections;

    public int final;

    public SpritesAndColliders[] BlockSettings;

    public bool blockcanupdate;

    private float updatetimer = 0.2f;

    private void Awake()
    {
        SpriteObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    virtual public void Update()
    {
        if (canupdate)
        {
            updatetimer -= Time.deltaTime;
        }

        if (updatetimer <= 0)
        {
            
            StartCoroutine("UB");
            updatetimer = 0.2f;
        }


    }

    public void PingBlocks()
    {
        int layerMask = 1 << 9;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.7f, layerMask))
        {

            if (hit.collider.tag == gameObject.tag)
                hit.collider.gameObject.GetComponent<BlockBase>().UpdateBlock();

            GetComponent<BlockBase>().UpdateBlock();

        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 0.7f, layerMask))
        {

            if (hit.collider.tag == gameObject.tag)
                hit.collider.gameObject.GetComponent<BlockBase>().UpdateBlock();

            GetComponent<BlockBase>().UpdateBlock();
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 0.7f, layerMask))
        {

            if (hit.collider.tag == gameObject.tag)
                hit.collider.gameObject.GetComponent<BlockBase>().UpdateBlock();

            GetComponent<BlockBase>().UpdateBlock();
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 0.7f, layerMask))
        {

            if (hit.collider.tag == gameObject.tag)
                hit.collider.gameObject.GetComponent<BlockBase>().UpdateBlock();

            GetComponent<BlockBase>().UpdateBlock();
        }
    }

    virtual public void UpdateBlock()
    {
       
        StartCoroutine("UB");

       
    }

    virtual public IEnumerator UB()
    {
        SpriteObj.GetComponent<SpriteRenderer>().sortingOrder = 8;
        yield return new WaitForSeconds(.02f);
        BlockSettings[final].BoxColl.SetActive(false);
        upblock = false;
        rightblock = false;
        leftblock = false;
        downblock = false;

        connections = 0;
        final = 0;
        canupdate = true;
        RaycastHit hit;
        LayerMask l = 1 << 9;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.7f,l))
        {
            if(hit.collider.tag == gameObject.tag)
            {
                blockrotation = rotation.zero;
                connections++;
                upblock = true;
            }
           
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 0.7f,l))
        {

            if (hit.collider.tag == gameObject.tag)
            {
                if (upblock != true)
                {
                    blockrotation = rotation.three;
                }
                connections++;
                rightblock = true;
            }
        }


        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 0.7f,l))
        {

                if (hit.collider.tag == gameObject.tag)
                {
                    if (upblock != true || rightblock != true)
                    {
                        blockrotation = rotation.six;
                    }
                    connections++;
                    downblock = true;
                }
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 0.7f,l))
        {

                    if (hit.collider.tag == gameObject.tag)
                    {
                        if (upblock != true || rightblock != true || downblock != true)
                        {
                            blockrotation = rotation.nine;
                        }
                        connections++;
                        leftblock = true;
                    }
        }

        BlockCalculation(connections);
        yield return null;
    }

    public void BlockCalculation(int i)
    {
        Debug.Log("shit");
        switch (i)
        {
            case 0:
                final = 0;
                Debug.Log("zero");
                break;
            case 1:
                final = 1;
                break;
            case 2:
                if (upblock == rightblock)
                {
                    final = 2;
                }
                else if(rightblock == downblock)
                {
                    final = 2;
                }
                else final = 3;
                break;
            case 3:
                if (!upblock) blockrotation = rotation.zero;
                else if (!rightblock) blockrotation = rotation.three;
                else if (!downblock) blockrotation = rotation.six;
                else blockrotation = rotation.nine;

                final = 4;
                break;
            case 4:
                final = 5;
                break;
        }
        BlockSettings[final].BoxColl.SetActive(true);
        SpriteObj.GetComponent<SpriteRenderer>().sprite = BlockSettings[final].Texture;
        BlockSet();
    }

    public void BlockSet()
    {
        switch (blockrotation)
        {
            case rotation.zero:
                break;
            case rotation.three:
                transform.Rotate(new Vector3(0, 90, 0)); 
                break;
            case rotation.six:
                transform.Rotate(new Vector3(0, 180, 0));
                break;
            case rotation.nine:
                transform.Rotate(new Vector3(0, -90, 0));
                break;
        }

       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            DIE();
            Destroy(collision.gameObject);
        }
    }

    virtual public void DIE()
    {
        gameObject.layer = 1;
        LayerMask l = 1 >> 14;
        if (Vector3.Distance(transform.position, GameManager.Player.transform.position) <= 0.6f)
        {
            SceneManager.LoadScene(3);
            Destroy(GameManager.Player);
        }
        PingBlocks();
        
        Destroy(gameObject);
    }
}
