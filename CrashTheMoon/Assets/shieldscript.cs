using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldscript : MonoBehaviour
{
    public GameObject dad;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            Destroy(collision.gameObject);
            Destroy(dad);
        }
    }
}
