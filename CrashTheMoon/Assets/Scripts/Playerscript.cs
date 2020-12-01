using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerscript : MonoBehaviour
{
    public float movementspeed;
    public Vector3 _inputs;

    public GameObject Ship;
    private Vector3 shipMove;
    private bool isfiring;

    public GameObject GrapplePoint;

    public float grapplerange;
    public bool blockGrappled;

    public bool willretract;
    public float retractspeed;
    public float blockretractspeed;
    public float normalretractspeed;

    private Transform extendpos;
    public GameObject Grapple;
    public Coroutine co;

    public enum Grapplestage { idle, extend, retract};
    public Grapplestage GS;

    public Vector3 extendTo;
    public bool lookForBlocks;

    public LineRenderer ln;

    public float grappleSpeed;
    private Animator controller;

    public bool up;
    public bool down;
    public bool side;
    public bool updiag;
    public bool downdiag;
    public bool idle;

    public bool IsControlled = false;
    public void Awake()
    {
        controller = GetComponentInChildren<Animator>();
        GameManager.Player = this.gameObject;
        ln.enabled = false;
        //Ship = GameManager.gm.ship;
       
    }

    public void Update()
    {
        if (lookForBlocks) LookForBlocks();
        if (isfiring)
        {
            ln.SetPosition(0, transform.position);
            ln.SetPosition(1, GrapplePoint.transform.position);
        }
        InputFind();
        moveChar();
        GrappleUpdate();
    }
    
    void GrappleUpdate()
    {
        switch (GS)
        {
            case Grapplestage.idle:
                break;
            case Grapplestage.extend:
                ExtendUpdate();
                break;
            case Grapplestage.retract:
                retract();
                break;
                
        }
    }

    void InputFind()
    {

        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    IsControlled = !IsControlled;
        //}

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                LayerMask Layer = 1 << 12;


                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, Layer))
                {

                    Extend(hit.point);
                }


            }
            this._inputs.x = Input.GetAxis("Horizontal");
            this._inputs.z = Input.GetAxis("Vertical");
            LayerMask lm = 1 << 14;
            if (Physics.Raycast(transform.position, Vector3.forward, 0.1f, lm) && _inputs.z > 0) 
            {
                Debug.LogError("it");
                _inputs.z = 0;
            }
            if (Physics.Raycast(transform.position, Vector3.back, 0.3f, lm) && _inputs.z < 0)
            {
                _inputs.z = 0;
            }
            if (Physics.Raycast(transform.position, Vector3.right, 0.2f, lm) && _inputs.x > 0)
            {
                _inputs.x = 0;
            }
            if (Physics.Raycast(transform.position, Vector3.left, 0.2f, lm) && _inputs.x < 0)
            {
                _inputs.x = 0;
            }

            this._inputs = Vector3.ClampMagnitude(this._inputs, 1f);

            

            if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(Input.GetAxis("Horizontal") > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            UpdateAnims();
        

        //if (!IsControlled)
        //{
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        StartCoroutine(RotateMe(Vector3.up * 90, 0.8f));
        //    }
        //    if (Input.GetKeyDown(KeyCode.Q))
        //    {
        //        StartCoroutine(RotateMe(Vector3.up * -90, 0.8f));
        //    }
        //}
    }

    //IEnumerator RotateMe(Vector3 byAngles, float inTime)
    //{
    //    var fromAngle = transform.rotation;
    //    var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
    //    for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
    //    {
    //        GameManager.ship.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
    //        yield return null;
    //    }
    //}
  

    void UpdateAnims()
    {
        
        if (_inputs.x != 0 && _inputs.z == 0&& !side)
        {
            controller.SetBool("gosideways", true);
            side = false;
        }
        else controller.SetBool("gosideways", false);

        if (_inputs.x == 0 && _inputs.z > 0&&!up)
        {
            controller.SetBool("goup", true);
            up = false;
        }
        else controller.SetBool("goup", false);
        
        if (_inputs.x == 0 && _inputs.z < 0&&!down)
        {
            controller.SetBool("godown", true);
            down = false;
        }
        else controller.SetBool("godown", false);

        if (_inputs.x == 0 && _inputs.z == 0&&!idle)
        {
            controller.SetBool("playeridle", true);
            idle = false;
        }
        else controller.SetBool("playeridle", false);
          
        if (_inputs.x != 0 && _inputs.z > 0&&!updiag)
        {
            controller.SetBool("godiagup", true);
            updiag = false;
        }
        else controller.SetBool("godiagup", false);

        if (_inputs.x != 0 && _inputs.z < 0 && !downdiag)
        {
            controller.SetBool("godiagdown", true);
            downdiag = false;
        }
        else controller.SetBool("godiagdown", false);


    }

    void moveChar()
    {
        if(_inputs != Vector3.zero && !isfiring)
        transform.Translate(_inputs * movementspeed * Time.deltaTime);
    }


    void Extend(Vector3 A)
    {
        GrapplePoint = Object.Instantiate(Grapple, transform.position, transform.rotation);
        ln.enabled = true;
        isfiring = true;
        extendTo = A;
        GS = Grapplestage.extend;
        retractspeed = normalretractspeed;
    }

    void ExtendUpdate()
    {
            GrapplePoint.transform.position = Vector3.MoveTowards(GrapplePoint.transform.position, extendTo, grappleSpeed * Time.deltaTime);
            if (Vector3.Distance(extendTo, GrapplePoint.transform.position) < 0.01)
            {
            
            GS = Grapplestage.retract;
            }
    }

    public void LookForBlocks()
    {    
                Debug.LogError("wait this works");
                retractspeed = blockretractspeed;
                GS = Grapplestage.retract;
    }

    void retract()
    {
        
        if (Vector3.Distance(transform.position, GrapplePoint.transform.position) > 0.01)
        {
            GrapplePoint.transform.position = Vector3.MoveTowards(GrapplePoint.transform.position, transform.position, retractspeed  * Time.deltaTime);
        }
        else
        {
            grappleReset();
        }

    }

    public void grappleReset()
    {

        ln.enabled = false;
        Destroy(GrapplePoint);
        GS = Grapplestage.idle;
        isfiring = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 17)
        {
            Destroy(gameObject);
        }
    }
}