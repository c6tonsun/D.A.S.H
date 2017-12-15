using UnityEngine;
// next line enables use of the operating system's serialization capabilities within the script
using System.Runtime.Serialization.Formatters.Binary;
// next line, IO stands for Input/Output, and is what allows us to write to and read from
// our computer or mobile device. Allowing to create unique files and then read them.
using System.IO;

public static class Settings {

    public const string FILE_PATH = "/DASHsettings.gd";

    public static float Volume { get; set; }

    private static bool FindSettings()
    {
        if (File.Exists(Application.persistentDataPath + FILE_PATH))
        {
            return true;
        }

        return false;
    }

    private static void MakeSettings()
    {
        Volume = 1f;
    }

    private static void SaveSettings()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + FILE_PATH);
        bf.Serialize(file, Volume);
        file.Close();
    }

    private static void LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + FILE_PATH))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + FILE_PATH, FileMode.Open);
            Volume = (float) bf.Deserialize(file);
            file.Close();
        }
    }

    private static void DeleteSettings()
    {
        if (File.Exists(Application.persistentDataPath + FILE_PATH))
        {
            File.Delete(Application.persistentDataPath + FILE_PATH);
        }
    }

    public static void LoadVolume()
    {
        if (!FindSettings())
        {
            MakeSettings();
            SaveSettings();
        }
        LoadSettings();
    }

    public static void SetVolume(float volume)
    {
        Volume = Mathf.Clamp(volume, 0f, 1f);
        SaveSettings();
        LoadSettings();
    }
}
