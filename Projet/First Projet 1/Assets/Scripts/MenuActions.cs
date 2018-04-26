using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour {

    public Dropdown LvlChoice;
    private List<int> ScenesBuild;
	
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
        if (ScenesBuild.Contains(Choice))
        {
<<<<<<< HEAD

            PhotonNetwork.room.IsOpen = false;
=======
            PhotonNetwork.room.open = false;
>>>>>>> parent of 3bb08cf... build
            SceneManager.LoadScene(Choice);
        }
    }
    
    public void DoQuit()
    {
        Debug.Log("sdfjdsbfls sdkf ");
        Application.Quit();
    }
}
