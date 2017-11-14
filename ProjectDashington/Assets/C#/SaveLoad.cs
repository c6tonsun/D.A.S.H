using UnityEngine;
// next line enables use of the operating system's serialization capabilities within the script
using System.Runtime.Serialization.Formatters.Binary;
// next line, IO stands for Input/Output, and is what allows us to write to and read from
// our computer or mobile device. Allowing to create unique files and then read them.
using System.IO;

public static class SaveLoad {

    public const int WORLD = 0;
    public const int LEVEL = 1;
    public const int OPEN = 2;
    public const int STAR = 3;

    public const int FALSE = 0;
    public const int TRUE = 1;

    public const string FILE_PATH = "/DASHsave.gd";

    public static int[,] SaveFile { get; set; }

    public static bool FindSaveFile()
    {
        if (File.Exists(Application.persistentDataPath + FILE_PATH))
        {
            return true;
        }

        return false;
    }

    public static void MakeSaveFile(int worldCount, int levelsPerWorld)
    {
        int[,] newSaveFile = new int[worldCount * levelsPerWorld, 4];

        int currentWolrd = 1;
        int currentLevel = 0;

        for (int i = 0; i < newSaveFile.Length / 4; i++)
        {
            newSaveFile[i, WORLD] = currentWolrd;
            newSaveFile[i, LEVEL] = currentLevel;
            newSaveFile[i, OPEN] = FALSE;
            newSaveFile[i, STAR] = FALSE;
            currentLevel++;

            if (currentLevel > levelsPerWorld)
            {
                currentWolrd++;
                currentLevel = 0;
            }
        }

        newSaveFile[0, OPEN] = TRUE;   // opens world 1
        newSaveFile[1, OPEN] = TRUE;   // opens level 1

        SaveFile = newSaveFile;
    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + FILE_PATH);
        bf.Serialize(file, SaveFile);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + FILE_PATH))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + FILE_PATH, FileMode.Open);
            SaveFile = (int[,])bf.Deserialize(file);
            file.Close();
        }
    }
}
