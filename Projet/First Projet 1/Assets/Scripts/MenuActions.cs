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

        //if (photonView.isMine && IsABoy.isOn)
          //  GetComponent<PlayerClass>().GetPlayerPrefab = GetComponent<>()
        
        /*foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<PlayerClass>().GetPlayerName == "Player One")
            {
                if (IsABoy.isOn)
                {
                    player.GetComponent<PlayerClass>().GetPlayerName = "TheBoy";
                    player.GetComponent<PlayerClass>().GetPlayerPrefab = player.GetComponent<PlayerClass>().GetPrefabBoyName;
                }
                else
                {
                    player.GetComponent<PlayerClass>().GetPlayerName = "TheGirl";
                    player.GetComponent<PlayerClass>().GetPlayerPrefab = player.GetComponent<PlayerClass>().GetPrefabGirlName;
                }
            }
            else
            {
                if (IsABoy.isOn)
                {
                    player.GetComponent<PlayerClass>().GetPlayerName = "TheGirl";
                    player.GetComponent<PlayerClass>().GetPlayerPrefab = player.GetComponent<PlayerClass>().GetPrefabGirlName;
                }
                else
                {
                    player.GetComponent<PlayerClass>().GetPlayerName = "TheBoy";
                    player.GetComponent<PlayerClass>().GetPlayerPrefab = player.GetComponent<PlayerClass>().GetPrefabBoyName;
                }
            }
        }*/
        foreach (PlayerClass player in FindObjectsOfType<PlayerClass>())
        {
            if (player.GetPlayerName == "Player One")
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
