using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private int[,] saveFile;

    public int worldCount;
    public int levelsPerWorld;

    public int world;
    public int level;

    public string menuMode;
    public const string LEVEL_MENU = "Level";

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
            SaveLoad.MakeSaveFile(worldCount, levelsPerWorld);
        }

        saveFile = SaveLoad.SaveFile;
    }

    // public methods

    public void StartLevel()
    {
        if (SceneManager.GetSceneByName("World " + world) != null)
        {
            SceneManager.LoadScene("World " + world);
        }
    }

    public void LoadMenu(string menuMode)
    {
        this.menuMode = menuMode;
        SceneManager.LoadScene("Main menu");
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
}
