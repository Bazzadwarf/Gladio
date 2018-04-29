using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts;

public class UIWeaponSelector : MonoBehaviour {

    public Button HeaterSheildButton;
    public Button RoundShieldButton;
    public Button TowerShildButton;
    public Button HammerButton;
    public Button MaceButton;
    public Button SwordButton;

    public GameObject HeaterSheild;
    public GameObject RoundShield;
    public GameObject TowerShield;
    public GameObject Hammer;
    public GameObject Mace;
    public GameObject Sword;

    Button[] _buttons = new Button[6];

    void Start()
    {
        _buttons = new Button[6] { HeaterSheildButton, RoundShieldButton, TowerShildButton, HammerButton, MaceButton, SwordButton };
    }

    public void WeaponSelector(GameObject weapon)
    {

        if (Data.Count == 0)
        {
            Data.Weapon1 = weapon;
            Data.Count++;
        }
        else if(Data.Count == 1)
        {
            Data.Weapon2 = weapon;
            Data.Count++;
        }
        else
        {
            Data.Weapon1 = Data.Weapon2;
            Data.Weapon2 = weapon;
        }

    }

    public void LoadLevel()
    {
        if (Data.Weapon1 != null && Data.Weapon2 != null)
        {
            SceneManager.LoadScene("Game",LoadSceneMode.Single);
        }
    }

    void UpdateButtons()
    {
        
    }
}
