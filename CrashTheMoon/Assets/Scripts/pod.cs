using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pod : MonoBehaviour
{
    public void Start()
    {
        GameManager.ship = this.gameObject;
    }

    
}
