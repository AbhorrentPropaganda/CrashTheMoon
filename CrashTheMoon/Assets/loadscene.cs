using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadscene : BlockBase
{
    public void load()
    {
        SceneManager.LoadScene(0);
    }

    public override void DIE()
    {
        SceneManager.LoadScene(3);
        base.DIE();
    }
}
