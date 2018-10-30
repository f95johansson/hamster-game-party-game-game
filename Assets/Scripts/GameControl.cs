using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

    public static GameControl control;

    public float health;
    public float experience;

    private void Awake()
    {
        if(control == null) {
            DontDestroyOnLoad(gameObject);
            control = this;
        }else if(control != this){
            Destroy(gameObject);
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "health: " + health);
        GUI.Label(new Rect(10, 40, 100, 30), "experience: " + experience);
    }

    public void save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        playerData data = new playerData();
        data.health = health;
        data.experience = experience;

        bf.Serialize(file, data);
        file.Close();
    }

    public void load() {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            playerData data = (playerData)bf.Deserialize(file);
            file.Close();
            health = data.health;
            experience = data.experience;
        }
    }
}

[Serializable]
class playerData {
    public float health;
    public float experience;
}

