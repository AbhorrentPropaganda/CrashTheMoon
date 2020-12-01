using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundmove : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.down * 0.05f * Time.deltaTime);
    }
}
