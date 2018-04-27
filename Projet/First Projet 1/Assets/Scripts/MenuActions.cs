using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuActions : Photon.MonoBehaviour {

    public Dropdown LvlChoice;
    private List<int> ScenesBuild;
    [SerializeField] private Toggle IsABoy;
	
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
        int Choice = LvlChoice.value + 1;

        foreach (PlayerClass player in FindObjectsOfType<PlayerClass>())
        {
            if (player.GetPlayerName == "PlayerOne")
            {
                if (IsABoy.isOn)
                {
                    player.GetPlayerName = "TheBoy";
                    player.GetPlayerPrefab = player.GetPrefabBoyName;
                }
                else
                {
                    player.GetPlayerName = "TheGirl";
                    player.GetPlayerPrefab = player.GetPrefabGirlName;
                }
            }
            else
            {
                if (IsABoy.isOn)
                {
                    player.GetPlayerName = "TheGirl";
                    player.GetPlayerPrefab = player.GetPrefabGirlName;
                }
                else
                {
                    player.GetPlayerName = "TheBoy";
                    player.GetPlayerPrefab = player.GetPrefabBoyName;
                }
            }
        }
        if (ScenesBuild.Contains(Choice))
        {
            PhotonNetwork.room.IsOpen = false;
            SceneManager.LoadScene(Choice);
        }
    }
    
    public void DoQuit()
    {
        Debug.Log("sdfjdsbfls sdkf ");
        Application.Quit();
    }
}
