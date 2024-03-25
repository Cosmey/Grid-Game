//coded by reece

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveDataScript : MonoBehaviour
{
    private static SaveData mySaveData;
    // Start is called before the first frame update
    void Awake()
    {
        LoadData();
    }
    void OnDestroy()
    {
        SaveData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DataSaver.Delete();
            Debug.Log("deleted save data");
        }
    }

    private void LoadData()
    {
        mySaveData = new SaveData();
        mySaveData = DataSaver.Load(mySaveData);
    }
    private void SaveData()
    {
        DataSaver.Save(mySaveData);
    }

    public static void SetKeyCode(string keyCodeName,KeyCode newCode)
    {
        if(keyCodeName == "toggleTowerPlacingKeyCode")
        {
            mySaveData.toggleTowerPlacingKeyCode = newCode;
        } 
        else if (keyCodeName == "cycleTowerKeyCode")
        {
            mySaveData.cycleTowerKeyCode = newCode;
        }
        else if (keyCodeName == "basicTowerKeyCode")
        {
            mySaveData.basicTowerKeyCode = newCode;
        }
        else if (keyCodeName == "wallTowerKeyCode")
        {
            mySaveData.wallTowerKeyCode = newCode;
        }
        else if (keyCodeName == "moveLeftKeyCode")
        {
            mySaveData.moveLeftKeyCode = newCode;
        }
        else if (keyCodeName == "moveRightKeyCode")
        {
            mySaveData.moveRightKeyCode = newCode;
        }
        else if (keyCodeName == "moveUpKeyCode")
        {
            mySaveData.moveUpKeyCode = newCode;
        }
        else if (keyCodeName == "moveDownKeyCode")
        {
            mySaveData.moveDownKeyCode = newCode;
        }

        GameObject player = GameObject.Find("Main Camera");
        if(player.GetComponent<PlayerControllerScript>() != null)
        {
            player.GetComponent<PlayerControllerScript>().LoadControls();
        }
    }
    public static KeyCode GetKeyCode(string keyCodeName)
    {
        if (keyCodeName == "toggleTowerPlacingKeyCode")
        {
            return mySaveData.toggleTowerPlacingKeyCode;
        }
        else if (keyCodeName == "cycleTowerKeyCode")
        {
            return mySaveData.cycleTowerKeyCode;
        }
        else if (keyCodeName == "basicTowerKeyCode")
        {
            return mySaveData.basicTowerKeyCode;
        }
        else if (keyCodeName == "wallTowerKeyCode")
        {
            return mySaveData.wallTowerKeyCode;
        }
        else if (keyCodeName == "moveLeftKeyCode")
        {
            return mySaveData.moveLeftKeyCode;
        }
        else if (keyCodeName == "moveRightKeyCode")
        {
            return mySaveData.moveRightKeyCode;
        }
        else if (keyCodeName == "moveUpKeyCode")
        {
            return mySaveData.moveUpKeyCode;
        }
        else if (keyCodeName == "moveDownKeyCode")
        {
            return mySaveData.moveDownKeyCode;
        }
        return KeyCode.P;
    }
    public static void SetVolume(string volumeType,float newVolume)
    {
        if(volumeType == "master")
        {
            mySaveData.masterVolume = newVolume;
        }
        else if (volumeType == "SFX")
        {
            mySaveData.SFXVolume = newVolume;
        }
        else if (volumeType == "music")
        {
            mySaveData.musicVolume = newVolume;
        }
    }
    public static float GetVolume(string volumeType)
    {
        if (volumeType == "master")
        {
            return mySaveData.masterVolume;
        }
        else if (volumeType == "SFX")
        {
            return mySaveData.SFXVolume;
        }
        else if (volumeType == "music")
        {
            return mySaveData.musicVolume;
        }
        return 0;
    }
}

[Serializable]
public class SaveData
{
    public KeyCode toggleTowerPlacingKeyCode = KeyCode.Alpha0;
    public KeyCode cycleTowerKeyCode = KeyCode.Tab;
    public KeyCode basicTowerKeyCode = KeyCode.Q;
    public KeyCode wallTowerKeyCode = KeyCode.E;

    public KeyCode moveLeftKeyCode = KeyCode.A;
    public KeyCode moveRightKeyCode = KeyCode.D;
    public KeyCode moveUpKeyCode = KeyCode.W;
    public KeyCode moveDownKeyCode = KeyCode.S;

    public float masterVolume = 0.2f;
    public float SFXVolume = 0.2f;
    public float musicVolume = 0.2f;
}



public class DataSaver
{
    public static void Save(SaveData myData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/SaveData.dat");
        bf.Serialize(fs, myData);
        fs.Close();
    }
    public static SaveData Load(SaveData myData)
    {
        if(File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.persistentDataPath + "/SaveData.dat",FileMode.Open);
            myData = bf.Deserialize(fs) as SaveData;
            fs.Close();
        }
        return myData;
    }
    public static void Delete()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/SaveData.dat");
        }
    }
}

