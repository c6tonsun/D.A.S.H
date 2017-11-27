using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private UIManager _UIManager;
    private WorldManager _worldManager;

    private int[,] saveFile;
    public int w1LevelCount;
    public int w2LevelCount;
    public int w3LevelCount;

    public int world;
    public int level;

    private bool _isPaused;

    public string menuMode;
    public const string LEVEL_MENU = "Level";
    public const string GAME_UI = "Game";
    public const string PAUSE_UI = "Pause";
    public const string WIN_UI = "Win";
    public const string LOSE_UI = "Lose";

    public const int ENEMY_LAYER = 10;
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";
    public const string SHIELD_TAG = "Shield";

    private void Start()
    {
        if (FindObjectsOfType<GameManager>().Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _UIManager = FindObjectOfType<UIManager>();

        GetSaveFileFromMemory();
    }

    private void GetSaveFileFromMemory()
    {
        if (SaveLoad.FindSaveFile())
        {
            SaveLoad.Load();
        }
        else
        {
            SaveLoad.MakeSaveFile(w1LevelCount, w2LevelCount, w3LevelCount);
        }

        saveFile = SaveLoad.SaveFile;
    }

    // public methods

    public void StartWorldAndLevel()
    {
        SceneManager.LoadScene("World " + world);
    }

    public void LoadMenu(string menuMode)
    {
        this.menuMode = menuMode;
        _UIManager.UpdateMenu();

        SceneManager.LoadScene("Main menu");
    }

    public void StartLevel()
    {
        _worldManager.RestartLevel();
        _UIManager.StartLevel();
    }

    public void NextLevel()
    {
        level++;
        _worldManager.RestartLevel();
        _UIManager.StartLevel();
    }

    public void InitializeGameUI()
    {
        _UIManager.StartLevel();
    }

    // Getters and setters

    public void SetWorldValue(int value)
    {
        world = value;
    }

    public void SetLevelValue(int value)
    {
        level = value;
    }

    public int GetLevelValue()
    {
        return level;
    }

    public string GetLevelMenuString()
    {
        return LEVEL_MENU;
    }

    public void SetMenuMode(string menuMode)
    {
        this.menuMode = menuMode;
        _UIManager.UpdateMenu();
    }

    public void SetWorldManager(WorldManager worldManager)
    {
        _worldManager = worldManager;
    }

    public int GetStarValue()
    {
        return _worldManager.GetStarValue();
    }

    // On App Pause

    public void OnApplicationPause(bool pause)
    {
        _isPaused = pause;
        
        if (_UIManager != null)
        {
            if (_isPaused && menuMode == GAME_UI)
            {
                menuMode = PAUSE_UI;
                _UIManager.UpdateMenu();

                Time.timeScale = 0f;
            }
            else if (!_isPaused && menuMode == PAUSE_UI)
            {
                menuMode = GAME_UI;
                _UIManager.UpdateMenu();

                Time.timeScale = 1f;
            }
        }
    }
}
