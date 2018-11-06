using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

    public static GameControl control;

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
internal class TrackData
{
    public string Name;
    public State[] States;
}

[Serializable]
internal class Tracks
{
    public TrackData[] SavedTracks;
}
