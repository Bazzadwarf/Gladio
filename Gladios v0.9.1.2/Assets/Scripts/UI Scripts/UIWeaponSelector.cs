using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class UIWeaponSelector : MonoBehaviour {

    GameObject _selectedWeapon;
    GameObject _selectedShield;

    public void WeaponSelector(GameObject weapon)
    {
        Data.Weapon = weapon;
    }

    public void SheildSelector(GameObject shield)
    {

        Data.Shield = shield;
    }

    public void LoadLevel()
    {
        if (Data.Shield != null && Data.Weapon != null)
        {
            SceneManager.LoadScene("Game",LoadSceneMode.Single);
        }
    }
}
