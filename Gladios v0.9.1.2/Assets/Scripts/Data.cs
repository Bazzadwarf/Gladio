using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Data
    {
        static GameObject _weapon1;
        static GameObject _weapon2;
        static int _intCount = 0;

        static public GameObject Weapon1 { get { return _weapon1; } set { _weapon1 = value; } }
        static public GameObject Weapon2 { get { return _weapon2; } set { _weapon2 = value; } }
        static public int Count { get { return _intCount; } set { _intCount = value; } }



    }
}
