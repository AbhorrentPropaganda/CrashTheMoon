using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class comicscript : MonoBehaviour
{
    public GameObject[] comicpanel;
    private int counter = -1;
    public GameObject text;
    public void Update()
    {
        if (Input.anyKeyDown)
        {
            text.SetActive(false);
            counter++;
            if (counter > comicpanel.Length-1)
            {
                SceneManager.LoadScene(2);
            }
            comicpanel[counter].SetActive(true);

        }
    }
}
