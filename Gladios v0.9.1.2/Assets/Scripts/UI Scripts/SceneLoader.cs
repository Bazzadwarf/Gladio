using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class SceneLoader : MonoBehaviour
{
    
	public void Loader(string s)
    {
        Data.Weapon = null;
        Data.Shield = null;
        SceneManager.LoadScene(s);
    }
}
