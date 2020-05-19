using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float delayToSwitchScene;

    public string hubScene;
    public string witchScene;
    public string werewolfScene;
    public string ghostScene;

    public string deathScene;

    private int highScoreWitch = 0;
    private int highScoreGhost = 0;
    private int highScoreWerewolf = 0;

    public gamemodes myGamemode;

    private int lastScore;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        if (PlayerPrefs.HasKey("HighScoreWitch"))
        {
            highScoreWitch = PlayerPrefs.GetInt("HighScoreWitch");
            highScoreGhost = PlayerPrefs.GetInt("HighScoreGhost");
            highScoreWerewolf = PlayerPrefs.GetInt("HighScoreWerewolf");
        }
        else
        {
            Save();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator switchScene(gamemodes newMode)
    {
        yield return new WaitForSeconds(delayToSwitchScene);

        myGamemode = newMode;

        if (newMode == gamemodes.Hub)
        {
            SceneManager.LoadScene(hubScene);
        }

        if (newMode == gamemodes.Sorciere)
        {
            SceneManager.LoadScene(witchScene);
        }

        if (newMode == gamemodes.Fantome)
        {
            SceneManager.LoadScene(ghostScene);
        }

        if (newMode == gamemodes.LoupGarou)
        {
            SceneManager.LoadScene(werewolfScene);
        }
    }

    public IEnumerator DeathScreen(gamemodes newMode, int newLastScore)
    {
        yield return new WaitForSeconds(delayToSwitchScene);

        lastScore = newLastScore;

        if (newMode == gamemodes.Sorciere)
        {
            if (lastScore > highScoreWitch)
            {
                highScoreWitch = lastScore;
            }
        }

        if (newMode == gamemodes.Fantome)
        {
            if (lastScore > highScoreGhost)
            {
                highScoreGhost = lastScore;
            }
        }

        if (newMode == gamemodes.LoupGarou)
        {
            if (lastScore > highScoreWerewolf)
            {
                highScoreWerewolf = lastScore;
            }
        }

        SceneManager.LoadSceneAsync(deathScene);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("HighScoreWitch", highScoreWitch);
        PlayerPrefs.SetInt("HighScoreGhost", highScoreGhost);
        PlayerPrefs.SetInt("HighScoreWerewolf", highScoreWerewolf);
    }

    public void SetGameMode(gamemodes newGamemode)
    {
        myGamemode = newGamemode;
    }

    public gamemodes GetGameMode()
    {
        return myGamemode;
    }

    public int getLastScore()
    {
        return lastScore;
    }

    public int getHighScore()
    {
        if (myGamemode == gamemodes.Sorciere)
        {
            return highScoreWitch;
        }

        if (myGamemode == gamemodes.Fantome)
        {
            return highScoreGhost;
        }

        if (myGamemode == gamemodes.LoupGarou)
        {
            return highScoreWerewolf;
        }

        return 0;
    }
}
