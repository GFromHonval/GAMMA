﻿using System;
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
            ScenesBuild.Add(i);
        }
        
        Choice = LvlChoice.value + 2;
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
            if (PhotonNetwork.playerList.Length == 2 && !GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().IsPlayingLocal
                || PhotonNetwork.playerList.Length == 1 && GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().IsPlayingLocal)
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
