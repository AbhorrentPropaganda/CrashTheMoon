using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScript : MonoBehaviour
{
    public bool Hooked;
    public Vector3 WhereTo;
    public bool placed;

    public bool placeable;
    public GameObject follow;

    public enum State {homing, hooked, floating, placed};
    public State PieceState;

    public GameObject pod;
    public bool grappled;
    public float followspeed;
    public float blockspeed;

    public GameObject grappledsprite;

    private float timer = 25;

    public void Awake()
    {
        
    }
    public void Start()
    {
        
    }
    public void Update()
    {
        if (grappled)
        {
            Raycast();

            SnapToBlock();
            pod = GameManager.ship;
        }

        if (placed)
        {
            
            Destroy(grappledsprite);
            Destroy(this);
        }

        if (grappled && !placed)
        {
            FollowGrapple();
        }
        else if(!grappled && !placed)
        {
            timer -= Time.deltaTime;
            if(timer<= 0)
            {
                Destroy(gameObject);
            }
            transform.Translate(Vector3.back * blockspeed * Time.deltaTime, Space.World);
        }
    }

    void Raycast()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 9;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.7f, layerMask))
        {
            GameManager.Player.GetComponent<Playerscript>().grappleReset();
            PositionKnown(0, hit.collider.transform.position);
            transform.parent = pod.transform;
            gameObject.layer = 9;
            GetComponent<BlockBase>().PingBlocks();
            GetComponent<BlockBase>().placed = true;
            PieceState = State.placed;
            placed = true;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 0.7f, layerMask))
        {
            GameManager.Player.GetComponent<Playerscript>().grappleReset();
            PositionKnown(1, hit.collider.transform.position);
            transform.parent = pod.transform;
            gameObject.layer = 9;
            GetComponent<BlockBase>().PingBlocks();
            GetComponent<BlockBase>().placed = true;
            PieceState = State.placed;
            placed = true;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 0.7f, layerMask))
        {
            GameManager.Player.GetComponent<Playerscript>().grappleReset();
            PositionKnown(2, hit.collider.transform.position);
            transform.parent = pod.transform;
            gameObject.layer = 9;
            GetComponent<BlockBase>().PingBlocks();
            GetComponent<BlockBase>().placed = true;
            PieceState = State.placed;
            placed = true;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 0.7f, layerMask))
        {
            GameManager.Player.GetComponent<Playerscript>().grappleReset();
            PositionKnown(3, hit.collider.transform.position);
            transform.parent = pod.transform;
            gameObject.layer = 9;
            GetComponent<BlockBase>().PingBlocks();
            GetComponent<BlockBase>().placed = true;
            PieceState = State.placed;
            placed = true;
        }
    }


   
    public void PositionKnown(int dir, Vector3 BlockDetected)
    {
        switch (dir)
        {
            case 0:
                WhereTo = new Vector3(BlockDetected.x, BlockDetected.y, BlockDetected.z -1);
                break;
            case 1:
                WhereTo = new Vector3(BlockDetected.x -1, BlockDetected.y, BlockDetected.z);
                break;
            case 2:
                WhereTo = new Vector3(BlockDetected.x +1, BlockDetected.y, BlockDetected.z);
                break;
            case 3:
                WhereTo = new Vector3(BlockDetected.x, BlockDetected.y, BlockDetected.z +1);
                break;
        }
    }

    public void SnapToBlock()
    {
        if(WhereTo != Vector3.zero)
        {
            transform.position = WhereTo;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.tag == "grapple")
        {
            Debug.Log("fuck");
            grappledsprite.SetActive(true);
            follow = collision.gameObject;
            GameManager.Player.GetComponent<Playerscript>().LookForBlocks();
            collision.gameObject.GetComponent<Collider>().enabled = false;
            grappled = true;
        }
    }
    

    public void FollowGrapple()
    {
      
        transform.position = Vector3.Lerp(transform.position, follow.transform.position, followspeed * Time.deltaTime * 5);
    }
    
}
