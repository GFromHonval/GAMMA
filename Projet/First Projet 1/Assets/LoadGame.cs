using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{

	public InputField StringRoom;
	public GameObject DDOLRoomName;

	public void Play()
	{
		DDOLRoomName.GetComponent<RoomName>().GetNomRoom = StringRoom.name;
		SceneManager.LoadScene("Menu principal");
	}
}
