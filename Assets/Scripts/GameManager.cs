using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum runmodes { full, single, story}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float delayToSwitchScene;

    public string hubScene;
    public string witchScene;
    public string werewolfScene;
    public string ghostScene;
    public string menuScene;

    public string deathScene;

    private int highScoreWitch = 0;
    private int highScoreGhost = 0;
    private int highScoreWerewolf = 0;

    public gamemodes myGamemode;
    public runmodes myRunmode;
    private bool gotMyRunMode = false;

    private int lastScore;

    private int gamesMade = 0;

    public gamemodes[] fullRunSet;

    private int totalScore = 0;
    private int HighTotalScore = 0;

    public int[] GamesScore;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (PlayerPrefs.HasKey("HighScoreWitch"))
        {
            highScoreWitch = PlayerPrefs.GetInt("HighScoreWitch");
            highScoreGhost = PlayerPrefs.GetInt("HighScoreGhost");
            highScoreWerewolf = PlayerPrefs.GetInt("HighScoreWerewolf");
            HighTotalScore = PlayerPrefs.GetInt("HighTotalScore");
        }
        else
        {
            Save();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gamesMade);
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

        if (newMode == gamemodes.Menu)
        {
            ResetRunMode();
            SceneManager.LoadScene(menuScene);
        }
    }

    public IEnumerator DeathScreen(gamemodes newMode, int newLastScore)
    {
        yield return new WaitForSeconds(delayToSwitchScene);

        lastScore = newLastScore;

        GamesScore[gamesMade - 1] = lastScore;

        if (myRunmode == runmodes.full)
        {
            if (totalScore > HighTotalScore)
            {
                HighTotalScore = totalScore;
            }
        }

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

    public void AddTotalScore(int newValue)
    {
        totalScore += newValue;
        Debug.Log(totalScore);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("HighScoreWitch", highScoreWitch);
        PlayerPrefs.SetInt("HighScoreGhost", highScoreGhost);
        PlayerPrefs.SetInt("HighScoreWerewolf", highScoreWerewolf);
        PlayerPrefs.SetInt("HighTotalScore", HighTotalScore);
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

    public int GetGamesScore(int indexGame)
    {
        return GamesScore[indexGame];
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

    public void SetRunMode(runmodes newRunmode)
    {
        if (gotMyRunMode == false)
        {
            myRunmode = newRunmode;

            gotMyRunMode = true;
        }
    }

    public runmodes GetRunMode()
    {
        return myRunmode;
    }

    public void ResetRunMode()
    {
        gotMyRunMode = false;
        gamesMade = 0;
    }

    public int GetGamesMadeNumber()
    {
        return gamesMade;
    }

    public void AddGamesMade()
    {
        gamesMade++;
    }

    public void ResetGamesMade()
    {
        gamesMade = 0;
        Debug.Log(gamesMade);
    }

    public gamemodes GetNextDestination()
    {
        return fullRunSet[gamesMade];
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    public int GetHighTotalScore()
    {
        return HighTotalScore;
    }

    public void ResetTotalScore()
    {
        totalScore = 0;
    }
}