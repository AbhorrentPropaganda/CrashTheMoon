using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public Camera Cam;
    public GameObject mainPoint;
    public GameObject creditPoint;
    public bool whereto;

    public void Update()
    {
        if (!whereto)
        {
            Cam.transform.position = Vector3.Lerp(Cam.transform.position, mainPoint.transform.position, 2f * Time.deltaTime);
        }
        else Cam.transform.position = Vector3.Lerp(Cam.transform.position, creditPoint.transform.position, 2f * Time.deltaTime);
    }

    public void boolswitch()
    {
        if (whereto)
        {
            whereto = false;
        }
        else whereto = true;
    }

    public void SceneChange()
    {
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
    }
}
