using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public int totalblocks;
    public int thrusters;

    public List<GameObject> MovingParts = new List<GameObject>();
    public List<GameObject> TotalPieces = new List<GameObject>();

    private int thrustmod;
    private int basethrust;
    private int weightmod;
    public void UpdateStats()
    {
        thrusters = MovingParts.Count(o => o.tag == "tag");
        totalblocks = TotalPieces.Count();
    }
    
    public void Inputs()
    {
        if(Input.GetAxisRaw("Horiziontal")!= 0)
        {
            transform.Translate(Vector3.right * ((thrusters * thrustmod) * Input.GetAxis("Horizontal") - (totalblocks * weightmod)));
        }

        if (Input.GetAxisRaw("Horiziontal") != 0)
        {
            transform.Translate(Vector3.right * ((thrusters * thrustmod) * Input.GetAxis("Horizontal") - (totalblocks * weightmod)));
        }
    }
}
