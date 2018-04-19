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
    float endingScreen;

    private int secs;
    private int mins;

    // Use this for initialization
    void Start () {
        Debug.Log("Started");
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
        else if (endingScreen > 5)
        {
            SceneManager.LoadScene("MenuScene");
        }
        else
        {
            endingScreen += Time.deltaTime;
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
        secs = (int)(timeAlive % 60);
        mins = (int)timeAlive / 60;

        string tempSecs;
        if(secs < 10)
        {
            tempSecs = "0" + secs;
        }
        else
        {
            tempSecs = secs.ToString();
        }

        string tempMins;
        if(mins < 10)
        {
            tempMins = "0" + mins;
        }
        else
        {
            tempMins = mins.ToString();
        }

        timeAliveText.text = string.Concat(tempMins, ":", tempSecs);
    }

    void DisplayScore()
    {
        
        finalKillCount.text = killCounter.ToString();
        finalTimer.text = timeAliveText.text;
        finalScore.text = Mathf.RoundToInt(killCounter * timeAlive).ToString();
        hudPanel.SetActive(false);
        scoreScreen.SetActive(true);
    }
}
