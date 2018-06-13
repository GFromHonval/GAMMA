using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private float Time = 180.0f;
    private bool HasEnded = false;
    private Canvas GameOver;

    /*private string g = "Niveau musical 1";
    private string g = "Niveau musical 2";
    private string g = "Niveau musical 3";
    private string g = "Niveau musical 4";*/
    
    public float GetTime
    {
        get { return Time; }
        set { Time = value; }
    }

    private void Start()
    {
        GameOver = GameObject.Find("GameLogic/GameOverCanvas").GetComponent<Canvas>();
    }

    private void Update ()
    {
        Time -= UnityEngine.Time.deltaTime; 
        if (Time <= 0)
        {
            Time = 0;
            if (SceneManager.GetActiveScene().name != "Menu principal" &&
                SceneManager.GetActiveScene().name != "Menu without logic" && SceneManager.GetActiveScene().name != "Niveau musical 1" 
                && SceneManager.GetActiveScene().name != "Niveau musical 2" && SceneManager.GetActiveScene().name != "Niveau musical 3"
                && SceneManager.GetActiveScene().name != "Niveau musical 4")
            {
                GameOver.enabled = true;
                if (GameObject.FindGameObjectWithTag("PlayerBoy") != null)
                    GameObject.FindGameObjectWithTag("PlayerBoy").GetComponent<RotationPlayer>().LifePerso = 0;
                if (GameObject.FindGameObjectWithTag("PlayerGirl") != null)
                    GameObject.FindGameObjectWithTag("PlayerGirl").GetComponent<RotationPlayer>().LifePerso = 0;
            }
        }

        SceneManager.activeSceneChanged += ReloadTimer;
    }

    private void ReloadTimer(Scene prevscene, Scene nextScene)
    {
        if (nextScene.name != "Menu principal" &&
            nextScene.name != "Menu without logic")
        {
            Time = GameObject.Find("GameParameters").GetComponent<GameParameters>().Timer;
        }
    }
    
    
}