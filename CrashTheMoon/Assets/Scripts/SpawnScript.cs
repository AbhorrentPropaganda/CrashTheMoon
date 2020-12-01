using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnScript : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource AS;
    public enum Stage { build, meteor, alien, buildtwo, finalstorm};
    public Stage gamepart;

    private Stage h;

    public float stage1time;
    public float stage2time;
    public float stage3time;
    public float stage4time;
    public float stage5time;

    public float totalTime;

    public float timer;
    private float spawntimer;
    private int n;

    private AudioClip a;

    public GameObject[] Blocks;
    public GameObject[] Meteors;
    public GameObject[] alien;

    public GameObject UiMan;

    private bool b;

    public void Awake()
    {
        AS = GetComponent<AudioSource>();
        totalTime = stage1time + stage2time + stage3time + stage4time + stage5time;
        
    }

    public void Update()
    {
        if(AS.clip != a)
        {
            AS.Play();
            a = AS.clip;
        }
        
        timer += Time.deltaTime;

        if(timer <= stage1time)
        {
            AS.clip = clips[0];
            
            gamepart = Stage.build;
        }
        else if (timer > stage1time)
        {
            AS.clip = clips[1];
            gamepart = Stage.meteor;
            if (h != gamepart)
            {
                UiMan.GetComponent<UiManager>().FlashText(2, 0.5f);
                h = Stage.meteor;

            }

        }
        else if (timer > stage2time + stage1time)
        {
            AS.clip = clips[1];

            gamepart = Stage.alien;

            if (h != gamepart)
            {
                UiMan.GetComponent<UiManager>().FlashText(2, 0.5f);
                h = Stage.alien;

            }
        }
        else if (timer > stage3time + stage2time + stage1time)
        {
            
            AS.clip = clips[0];
            
            gamepart = Stage.buildtwo;
        }
        else if (timer > stage4time + stage3time + stage2time + stage1time)
        {
            AS.clip = clips[1];
            
           
            gamepart = Stage.finalstorm;

            if (h != gamepart)
            {
                UiMan.GetComponent<UiManager>().FlashText(4, 0.5f);
                h = Stage.meteor;

            }
        }
        
        if(timer > totalTime)
        {
            SceneManager.LoadScene(4);
        }

        enumManager();
    }

    public void enumManager()
    {
        switch (gamepart)
        {
            case Stage.build:
                
                
                spawnScript(Blocks, 10, 5, 10, 3);
                break;
            case Stage.meteor:
                
                
                spawnScript(Meteors, 10, 2, 8, 2);
                break;
            case Stage.alien:
                
                
                spawnScript(Blocks, 10, 1, 3, 1);
                break;
            case Stage.buildtwo:
               
                
                spawnScript(Blocks, 10, 3, 4, 5);
                break;
            case Stage.finalstorm:
                
                
                spawnScript(Meteors, 10, 1, 3, 5);
                break;
        }
    }

    public void spawnScript(GameObject[] ToSpawn, float WidthToSpawn, float TimerangeBetweenSpawns, float TimeMaxBetweenSpawns, int samespawn)
    {
        spawntimer -= Time.deltaTime;
        if(spawntimer<= 0)
        {
            int numbertospawn = Random.Range(1, samespawn);
            for(int i = 0; i<= numbertospawn; i++)
            {
                Vector3 a = new Vector3(Random.Range(-WidthToSpawn / 2, WidthToSpawn / 2), 0, 15);
                Vector3 position = a + GameManager.ship.transform.position;
                Instantiate(ToSpawn[Random.Range(0, ToSpawn.Length - 1)], position, Quaternion.identity);

            }
            spawntimer = Random.Range(TimerangeBetweenSpawns, TimeMaxBetweenSpawns);
        }
        
    }
}
