using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private MenuUI _menuUI;

    private int[,] saveFile;

    public int w1LevelCount;
    public int w2LevelCount;
    public int w3LevelCount;

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

        _menuUI = FindObjectOfType<MenuUI>();

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

    public void StartLevel()
    {
        _menuUI.gameObject.SetActive(false);

        if (SceneManager.GetSceneByName("World " + world) != null)
        {
            SceneManager.LoadScene("World " + world);
        }
    }

    public void LoadMenu(string menuMode)
    {
        this.menuMode = menuMode;

        _menuUI.gameObject.SetActive(true);

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

    public void SetMenuMode(string menuMode)
    {
        this.menuMode = menuMode;
        _menuUI.UpdateMenu();
    }
}
