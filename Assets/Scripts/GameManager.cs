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

    public int[] highScoreWitch;
    public int[] highScoreGhost;
    public int[] highScoreWerewolf;

    public int highScoreSize;

    public gamemodes myGamemode;
    public runmodes myRunmode;
    private bool gotMyRunMode = false;

    private int lastScore;

    private int gamesMade = 0;

    public gamemodes[] fullRunSet;

    private int totalScore = 0;
    public int[] HighTotalScore;

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
            for (int i = 0; i < highScoreSize; i++)
            {
                highScoreWitch[i] = PlayerPrefs.GetInt("HighScoreWitch" + i.ToString("0"), highScoreWitch[i]);
                highScoreGhost[i] = PlayerPrefs.GetInt("HighScoreGhost" + i.ToString("0"), highScoreGhost[i]);
                highScoreWerewolf[i] = PlayerPrefs.GetInt("HighScoreWerewolf" + i.ToString("0"), highScoreWerewolf[i]);
                HighTotalScore[i] =  PlayerPrefs.GetInt("HighTotalScore" + i.ToString("0"), HighTotalScore[i]);
            }
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
           for (int k = 0; k < HighTotalScore.Length; k ++)
            {
                if (totalScore > HighTotalScore[k])
                {
                    HighTotalScore[k] = totalScore;
                    k = HighTotalScore.Length;
                }
            }
        }

        if (newMode == gamemodes.Sorciere)
        {
            for (int l = 0; l < highScoreSize; l++)
            {
                if (lastScore > highScoreWitch[l])
                {
                    highScoreWitch[l] = lastScore;
                    l = HighTotalScore.Length;
                }
            }
        }

        if (newMode == gamemodes.Fantome)
        {
            for (int m = 0; m < highScoreSize; m++)
            {
                if (lastScore > highScoreGhost[m])
                {
                    highScoreGhost[m] = lastScore;
                    m = HighTotalScore.Length;
                }
            }
        }

        if (newMode == gamemodes.LoupGarou)
        {
            for (int n = 0; n < highScoreSize; n++)
            {
                if (lastScore > highScoreWerewolf[n])
                {
                    highScoreWerewolf[n] = lastScore;
                    n = HighTotalScore.Length;
                }
            }
        }

        SceneManager.LoadSceneAsync(deathScene);
    }

    public void AddTotalScore(int newValue)
    {
        totalScore += newValue;
    }

    public void Save()
    {
        for (int i = 0; i < highScoreSize; i ++)
        {
            PlayerPrefs.SetInt("HighScoreWitch" + i.ToString("0"), highScoreWitch[i]);
            PlayerPrefs.SetInt("HighScoreGhost" + i.ToString("0"), highScoreGhost[i]);
            PlayerPrefs.SetInt("HighScoreWerewolf" + i.ToString("0"), highScoreWerewolf[i]);
            PlayerPrefs.SetInt("HighTotalScore" + i.ToString("0"), HighTotalScore[i]);
        }
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

    public int[] getHighScore(gamemodes myNewGM)
    {
        if (myNewGM == gamemodes.Sorciere)
        {
            return highScoreWitch;
        }

        if (myNewGM == gamemodes.Fantome)
        {
            return highScoreGhost;
        }

        if (myNewGM == gamemodes.LoupGarou)
        {
            return highScoreWerewolf;
        }

        return highScoreWerewolf;
    }

    public int[] getHighScore()
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

        return highScoreWerewolf;
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

    public int[] GetHighTotalScore()
    {
        return HighTotalScore;
    }

    public void ResetTotalScore()
    {
        totalScore = 0;
    }
}