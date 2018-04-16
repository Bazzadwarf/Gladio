using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuitApplication : MonoBehaviour {

	public void CloseApplication(GameObject currentPanel) 
	{
		currentPanel.SetActive(false);
		Application.Quit();
	}
}
