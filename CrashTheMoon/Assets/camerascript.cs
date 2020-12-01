using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{
    private bool iscontrolled;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(GameManager.Player.transform.position.x, 10, GameManager.Player.transform.position.z), 1 + Time.deltaTime);
    }
}
