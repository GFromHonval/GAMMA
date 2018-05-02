using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuActions : Photon.MonoBehaviour {

    public Dropdown LvlChoice;
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

        if (IsABoy.isOn)
            GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().GetPrefabFirstPlayer = "PrefabBoy";
        else
            GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().GetPrefabFirstPlayer = "PrefabGirl";
            
        
        if (ScenesBuild.Contains(Choice))
        {
           PhotonNetwork.LoadLevel(Choice);
        }
    }
    
    public void DoQuit()
    {
        Debug.Log("sdfjdsbfls sdkf ");
        Application.Quit();
    }
}
