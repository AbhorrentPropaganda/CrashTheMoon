using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("PhaseBlink")]
    public GameObject UiText;

    [Header("RoadMapControl")]
    public Slider roadmapSlider;

    [Header("winscreen")]
    public GameObject WinScreen;
    public Image WinBackground;
    public Color backinitcolor = Color.clear;
    public Color backgroundcolor;

    public GameObject Spawnmanager;

    private string a;
    private float b;
    private float c;
    private bool d;

    private float timer;
    private float timer2;

    public void Update()
    {
        roadmapSlider.maxValue = Spawnmanager.gameObject.GetComponent<SpawnScript>().totalTime;
        roadmapSlider.value = Spawnmanager.gameObject.GetComponent<SpawnScript>().timer;
    }

    public void FlashText(float timeflashing, float timebetweenflashes)
    {
       
        b = timeflashing;
        c = timebetweenflashes;

        StartCoroutine("flash");

    }

    IEnumerable flash()
    {
        if (timer <= b)
        {
            timer += Time.deltaTime;
            timer2 += Time.deltaTime;
            if (timer2 >= c)
            {
                if (UiText == enabled)
                {
                    UiText.gameObject.SetActive(false);

                }
                else UiText.gameObject.SetActive(true);

                timer2 = 0;
            }
        }
        else yield return null;
    }

    
}
