using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour {

    public GameObject hudPanel;
    public float maxHealth;
    public Slider healthBar;
    public Text killCounterText;
    public Text timeAliveText; 

    public GameObject scoreScreen;
    public Text finalKillCount;
    public Text finalTimer;
    public Text finalScore;

    float currHealth;
    GameObject hitObject;
    bool inBox = false;

    public int killCounter;
    float timeAlive;

    private string secs;
    private string mins;

    float wait = 10;

    // Use this for initialization
    void Start () {
        currHealth = maxHealth;
        RefreshHealthBar();
        RefreshKillCounter();
    }
	
	// Update is called once per frame
	void Update () {
		if(currHealth > 0)
        {
            RefreshKillCounter();
            RefreshTimer();
        }
	}
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("test");
        hitObject = col.gameObject;
        if (hitObject.tag == "EnemyWeapon" && !inBox)
        {
            Debug.Log("hit " + hitObject.GetComponent<EnemyHitStrength>().hitStrength);
            currHealth -= hitObject.GetComponent<EnemyHitStrength>().hitStrength;
            inBox = true;
            RefreshHealthBar();
            if (currHealth <= 0)
            {
                Debug.Log("Dead");
                this.DisplayScore();
                while(wait > 0)
                {
                    wait -= Time.deltaTime;
                }
                SceneManager.LoadScene("MenuScene");
                //UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        Debug.Log("test2");
        if (hitObject == col.gameObject)
        {
            inBox = false;
        }
    }

    void RefreshHealthBar()
    {
        healthBar.value = (currHealth / maxHealth);
    }

    void RefreshKillCounter()
    {
        killCounterText.text = killCounter.ToString();
    }

    void RefreshTimer()
    {
        timeAlive += Time.deltaTime;
        secs = Mathf.RoundToInt((timeAlive % 60)).ToString();
        mins = Mathf.RoundToInt(((timeAlive % 60) % 60)).ToString();
        timeAliveText.text = (mins + ":" + secs);
    }

    void DisplayScore()
    {
        
        finalKillCount.text = killCounter.ToString();
        finalTimer.text = (mins + ":" + secs);
        finalScore.text = Mathf.RoundToInt(killCounter * timeAlive).ToString();
        hudPanel.SetActive(false);
        scoreScreen.SetActive(true);
    }
}
