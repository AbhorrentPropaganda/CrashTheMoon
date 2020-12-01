using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killflor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
}
