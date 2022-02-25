using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMana : MonoBehaviour
{
    private static GameMana instance;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int Difficulty = 0;


    public void ChangeMode(int mode)
    {
        Difficulty = mode;
        SceneManager.LoadScene("Main_Game");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main_Game");
    }

}
