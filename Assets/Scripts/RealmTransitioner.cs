using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RealmTransitioner : MonoBehaviour
{
    public void Fire_Load()
    {
        if(Portal.player_is_here == true)
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Ice_Load()
    {
        if (Portal.player_is_here == true)
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
