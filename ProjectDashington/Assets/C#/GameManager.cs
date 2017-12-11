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

    public string menuMode;
    public const string EXIT_MENU = "Exit";
    public const string CREDIT_MENU = "Credit";
    public const string MAIN_MENU = "Main";
    public const string WORLD_MENU = "World";
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

        SaveLoad.Delete();
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
        _UIManager.InitializeMenu();

        SceneManager.LoadScene("Main menu", LoadSceneMode.Single);
    }

    public void StartLevel()
    {
        _worldManager.RestartLevel();
        _UIManager.StartLevel();
    }

    public void NextLevel()
    {
        if (level == GetCurrentLevelCount())
        {
            menuMode = LEVEL_MENU;
            _UIManager.UpdateMenu();
        }
        else
        {
            level++;
            _worldManager.RestartLevel();
            _UIManager.StartLevel();
        }
    }

    public void InitializeGameUI()
    {
        _UIManager.StartLevel();
    }

    public void FindCamera()
    {
        _UIManager.FindCamera();
    }

    // saving

    public void OpenWorldLevel(int world, int level)
    {
        for (int i = 0; i < saveFile.Length / 4; i++)
        {
            if (saveFile[i, SaveLoad.WORLD] == world &&
                saveFile[i, SaveLoad.LEVEL] == level)
            {
                saveFile[i, SaveLoad.OPEN] = SaveLoad.TRUE;
            }
        }

        SaveLoad.SaveFile = saveFile;
        SaveLoad.Save();
    }

    public void StarWorldLevel(int world, int level)
    {
        for (int i = 0; i < saveFile.Length / 4; i++)
        {
            if (saveFile[i, SaveLoad.WORLD] == world &&
                saveFile[i, SaveLoad.LEVEL] == level)
            {
                saveFile[i, SaveLoad.STAR] = SaveLoad.TRUE;
            }
        }

        SaveLoad.SaveFile = saveFile;
        SaveLoad.Save();
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

    public int[,] GetSaveFile()
    {
        return saveFile;
    }
    
    public int GetCurrentLevelCount()
    {
        if (world == 1)
            return w1LevelCount;

        if (world == 2)
            return w2LevelCount;

        if (world == 3)
            return w3LevelCount;

        return 0;
    }

    // System events

    public void OnApplicationPause(bool pause)
    {
        if (_UIManager != null)
        {
            if (pause && menuMode == GAME_UI)
            {
                menuMode = PAUSE_UI;
                _UIManager.UpdateMenu();

                Time.timeScale = 0f;
            }
            else if (!pause && menuMode == PAUSE_UI)
            {
                menuMode = GAME_UI;
                _UIManager.UpdateMenu();

                Time.timeScale = 1f;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
