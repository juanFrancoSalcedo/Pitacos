﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DataSystem
{
    public static void SaveLevel(int _levelIndex)
    {
        if (_levelIndex !=0 && _levelIndex != 12)
        {
            PlayerPrefs.SetInt(KeySotrage.LASTLEVEL_I, _levelIndex);
        }

        if (_levelIndex == 11)
        {
            PlayerPrefs.SetInt(KeySotrage.LASTLEVEL_I,1);
        }
    }

    public static int LoadLevel()
    {
        return PlayerPrefs.GetInt(KeySotrage.LASTLEVEL_I,1);
    }

    public static void  SaveSoundState(bool state)
    {
        PlayerPrefs.SetInt(KeySotrage.SOUNDSTATE_I, (state)?1:0);
    }

    public static bool LoadSoundState()
    {
        if (PlayerPrefs.GetInt(KeySotrage.SOUNDSTATE_I) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
}

public static class KeySotrage
{
    public static string LASTLEVEL_I = "LASTLEVEL";
    public static string SOUNDSTATE_I = "SOUNDSTATE";
    public static string FIRSTOPEN_I = "FIRSTOPEN";
}
