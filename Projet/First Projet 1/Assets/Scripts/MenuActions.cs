using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using ExitGames.Demos.DemoAnimator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuActions : Photon.MonoBehaviour {

    public TMP_Dropdown LvlChoice;
    [SerializeField] private Toggle IsABoy;
    public GameObject WrongChoice;
    
    private List<int> ScenesBuild;
    private int Choice;
    private Animator animator;
    private Fading FadingScript;
    private float Waiting;
    private TextMeshProUGUI wrongChoice;
    
    private void Start()
    {
        Waiting = 2f;
        FadingScript = GameObject.Find("FadeTransition").GetComponent<Fading>();
        animator = GameObject.Find("FadeTransition").GetComponent<Animator>();
        wrongChoice = WrongChoice.GetComponent<TextMeshProUGUI>();
    }

    public void GotoLvL()
    {
        ScenesBuild = new List<int>();
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            //Pour plus tard maybe quand on mettra le nom des scenes au lieu de leur numero dans le build 
            //string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            //int lastSlash = scenePath.LastIndexOf("/");
            ScenesBuild.Add(i);
        }
        
        Choice = LvlChoice.value + 1;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (player.IsMasterClient)
            {
                if (IsABoy.isOn)
                    player.NickName = "FirstPlayerBoy";

                else
                    player.NickName = "FirstPlayerGirl";
            }
        }
        
        if (ScenesBuild.Contains(Choice) && Choice <= GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().GetLevelSuceeded)
        {
            animator.SetTrigger("FadeOut");
        }
        else
        {
            wrongChoice.enabled = true;
        }
    }

    public void  ChangeLevel()
    {
        PhotonNetwork.LoadLevel(Choice);
    }
    
    public void DoQuit()
    {
        Application.Quit();
    }
}
