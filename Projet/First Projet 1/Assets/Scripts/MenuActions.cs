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
    private List<int> ScenesBuild;
    [SerializeField] private Toggle IsABoy;
    private int Choice;
	
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
        
        if (ScenesBuild.Contains(Choice))
        {
           PhotonNetwork.LoadLevel(Choice);
        }
    }
    
    public void DoQuit()
    {
        Application.Quit();
    }
}
