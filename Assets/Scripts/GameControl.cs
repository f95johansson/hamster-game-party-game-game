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
    public Inventory inventory = new Inventory();
    public PlayerData playerData = new PlayerData();

    //From Berenice
    //public Inventory inventory;

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

        //data.inventory = inventory;

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

            //inventory = data.inventory;
        }
    }

    //Berenice : not sure about that yet
    public void loadInventory()
    {
        if (File.Exists(Application.persistentDataPath + "/Inventory.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(Application.persistentDataPath + "/Inventory.dat")) {
                FileStream file = File.Open(Application.persistentDataPath + "/Inventory.dat", FileMode.Open);
                Inventory data = (Inventory)bf.Deserialize(file);
                file.Close();

                inventory.foodAmount = data.foodAmount;
                inventory.moneyAmount = data.moneyAmount;
                inventory.hamsterStates = data.hamsterStates;
            }else {
                inventory.foodAmount = 0;
                inventory.moneyAmount = 100;
            }

            //data.inventory
        }
    }

    //Berenice : not sure about that yet
    public void saveInventory()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Inventory.dat");

        //playerData data = new playerData();
        Inventory data = new Inventory();
        data.foodAmount = inventory.foodAmount;
        data.moneyAmount = inventory.moneyAmount;
        data.hamsterStates = inventory.hamsterStates;
        //data.inventory = inventory;

        bf.Serialize(file, data);
        file.Close();
    }

    public void loadPlayerData() {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
            {
                FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);
                PlayerData data = (PlayerData)bf.Deserialize(file);
                file.Close();

                playerData.numberCarrotsAllowed = data.numberCarrotsAllowed;
                playerData.numberCatsAllowed = data.numberCatsAllowed;
            }

            //data.inventory
        }
    }

    public void savePlayerData() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.dat");

        //playerData data = new playerData();
        PlayerData data = new PlayerData();
        data.numberCarrotsAllowed = playerData.numberCarrotsAllowed;
        data.numberCatsAllowed = playerData.numberCatsAllowed;
        //data.inventory = inventory;

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

