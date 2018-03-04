using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public void Loader(string s)
    {
        SceneManager.LoadScene(s);
    }
}
