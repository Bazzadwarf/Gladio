﻿using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class SceneLoader : MonoBehaviour
{
    
	public void Loader(string s)
    {
        Data.Weapon1 = null;
        Data.Weapon2 = null;
        SceneManager.LoadScene(s);
    }
}
