using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePersos : MonoBehaviour
{
	public Toggle ThisToggle;
	public Toggle OtherToggle;
		
	// Update is called once per frame
	public void OnClick()
	{
		OtherToggle.isOn = !ThisToggle.isOn;
	}
}
