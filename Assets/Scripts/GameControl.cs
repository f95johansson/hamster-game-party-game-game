using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

    public static GameControl control;

    public float highScore;
    public float health;
    public float experience;
    public int carrots;
    public int cats;

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
        data.highScore = highScore;

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
            highScore = data.highScore;
        }
    }

    public void loadItemList() {
        if (File.Exists(Application.persistentDataPath + "/itemList.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/itemList.dat", FileMode.Open);
            itemList data = (itemList)bf.Deserialize(file);
            file.Close();
            carrots = data.carrots;
            cats = data.cats;
        }
    }

    public void saveItemList() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/itemList.dat");

        itemList data = new itemList();
        data.carrots = carrots;
        data.cats = cats;
        bf.Serialize(file, data);
        file.Close();
    }

    public void loadShop() {
        if (File.Exists(Application.persistentDataPath + "/shop.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/shop.dat", FileMode.Open);
            playerData data = (playerData)bf.Deserialize(file);
            file.Close();
        }
    }

    public void saveShop() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/shop.dat");

        playerData data = new playerData();
        //data.health = health;
        //data.experience = experience;
        //data.highScore = highScore;

        bf.Serialize(file, data);
        file.Close();
    }
}

[Serializable]
class playerData {
    public float health;
    public float experience;
    public float highScore;
}

[Serializable]
class track {

}

[Serializable]
class itemList {
    public int carrots;
    public int cats;

}

[Serializable]
class shop {

}

