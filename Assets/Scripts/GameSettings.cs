using UnityEngine;
using UnityEditor;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.ComponentModel;
using System.Text.RegularExpressions;

public static class GameSettings
{
    private static GameData current = null;
    private static string SaveFilePath = String.Format("{0}/{1}", Application.persistentDataPath, "save.gd");

    public static GameData Current
    {
        get
        {
            if (current == null)
            {
                current = Load();
                if (current == null)
                {
                    current = new GameData();
                }
                current.PropertyChanged += Current_PropertyChanged;
            }
            return current;
        }
    }

    private static void Current_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        Save();
    }

    private static GameData Load()
    {
        GameData data = null;
        if (File.Exists(SaveFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(SaveFilePath, FileMode.Open);
            data = bf.Deserialize(file) as GameData;
            file.Close();
        }
        ValidateData(data);
        return data;
    }

    private static void ValidateData(GameData data)
    {
        if (data != null)
        {
            var nickname = data.Nickname;
            var match = Regex.Match(nickname, @"[a-zA-Z0-9]+");
            if (!match.Success)
            {
                data.Nickname = null;
            }
        }
    }

    private static void Save()
    {
        if (current != null)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(SaveFilePath);
            bf.Serialize(file, current);
            file.Close();
        }
    }
}

[Serializable]
public class GameData : INotifyPropertyChanged
{
    private String nickname = null;
    private bool advancedShipUnlocked = false;
    private String selectedShip = null;
    private bool adsDisabled = false;
    private int highscore = 0;

    public event PropertyChangedEventHandler PropertyChanged;

    public String Nickname
    {
        get
        {
            return nickname;
        }
        set
        {
            if (nickname != value)
            {
                nickname = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Nickname"));
                }
            }
        }
    }
    public bool AdvancedShipUnlocked
    {
        get
        {
            return advancedShipUnlocked;
        }
        set
        {
            if (advancedShipUnlocked != value)
            {
                advancedShipUnlocked = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AdvancedShipUnlocked"));
                }
            }
        }
    }
    public String SelectedShip
    {
        get
        {
            return selectedShip;
        }
        set
        {
            if (selectedShip != value)
            {
                selectedShip = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedShip"));
                }
            }

        }
    }
    public bool AdsDisabled
    {
        get
        {
            return adsDisabled;
        }
        set
        {
            if (adsDisabled != value)
            {
                adsDisabled = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AdsDisabled"));
                }
            }
        }
    }
    public int Highscore
    {
        get
        {
            return highscore;
        }
        set
        {
            if (highscore != value)
            {
                highscore = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Highscore"));
                }
            }
        }
    }
}