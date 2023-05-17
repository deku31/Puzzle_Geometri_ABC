using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageScript : MonoBehaviour
{
    public float highscore;

    public void save()
    {
        PlayerPrefs.SetFloat("Input",highscore);
        Debug.Log("Save");
    }
    public void Load()
    {
        Debug.Log("Load");
        PlayerPrefs.GetFloat("Input", highscore);
        highscore = PlayerPrefs.GetFloat("Input");
    }
    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
