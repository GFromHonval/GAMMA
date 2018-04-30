using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{

	private void OnGUI()
	{
		GUI.Box(new Rect(900, 10, 40, 20), GetComponent<RotationPlayer>().GetLife.ToString("0"));
	}

}
