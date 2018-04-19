using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIPanelSwitcher : MonoBehaviour {
	
	public GameObject currentPanel;
	
	public void SwitchPanel(GameObject nextPanel)
	{
		currentPanel.SetActive(false);
		nextPanel.SetActive(true);
	}
}
