using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timer = 180.0f;
    private bool HasEnded = false;
    [SerializeField] private GameObject GameOver;
    

    private void Update ()
    {
        timer -= Time.deltaTime; //-1 à chaque seconde
        if (timer <= 0)
        {
            timer = 0;
            GameOver.SetActive(true);
            //GetComponent<PlayerController>().enabled = false;
            End_Restart.EndGame();
        }

    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 40, 20), timer.ToString("0"));
    }

}