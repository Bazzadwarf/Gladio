using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Data
    {
        static GameObject _weapon;
        static GameObject _shield;

        static public GameObject Weapon { get { return _weapon; } set { _weapon = value; } }
        static public GameObject Shield { get { return _shield; } set { _shield = value; } }



    }
}
