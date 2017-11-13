using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private int[,] saveFile;

    public int worldCount;
    public int levelsPerWorld;

    public int world;
    public int level;

    private void Start()
    {
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

    // Getters and setters

    public void SetWorldValue(int value)
    {
        world = value;
    }

    public void SetLevelValue(int value)
    {
        level = value;
    }
}
