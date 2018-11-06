using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

    public static GameControl Control;

    public Inventory Inventory = new Inventory();
    public PlayerData PlayerData = new PlayerData();

    //From Berenice
    //public Inventory inventory;

    private void Awake()
    {
        if(Control == null) {
            DontDestroyOnLoad(gameObject);
            Control = this;
        }else if(Control != this){
            Destroy(gameObject);
        }
    }

    //Berenice : not sure about that yet
    public void LoadInventory()
    {
        if (File.Exists(Application.persistentDataPath + "/Inventory.dat"))
        {
            var bf = new BinaryFormatter();
            var file = File.Open(Application.persistentDataPath + "/Inventory.dat", FileMode.Open);
            var data = (Inventory)bf.Deserialize(file);
            file.Close();

            Inventory.foodAmount = data.foodAmount;
            Inventory.moneyAmount = data.moneyAmount;
            Inventory.hamsterStates = data.hamsterStates;
        }
    }

    //Berenice : not sure about that yet
    public void SaveInventory()
    {
        var bf = new BinaryFormatter();
        var file = File.Create(Application.persistentDataPath + "/Inventory.dat");

        //playerData data = new playerData();
        var data = new Inventory
        {
            foodAmount = Inventory.foodAmount,
            moneyAmount = Inventory.moneyAmount,
            hamsterStates = Inventory.hamsterStates
        };

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadPlayerData() {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
        {
            var bf = new BinaryFormatter();
            if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
            {
                var file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);
                var data = (PlayerData)bf.Deserialize(file);
                file.Close();

                PlayerData.numberCarrotsAllowed = data.numberCarrotsAllowed;
                PlayerData.numberCatsAllowed = data.numberCatsAllowed;
            }
        }
    }

    public void SavePlayerData() {
        var bf = new BinaryFormatter();
        var file = File.Create(Application.persistentDataPath + "/PlayerData.dat");

        //playerData data = new playerData();
        var data = new PlayerData
        {
            numberCarrotsAllowed = PlayerData.numberCarrotsAllowed,
            numberCatsAllowed = PlayerData.numberCatsAllowed
        };
        //data.inventory = inventory;

        bf.Serialize(file, data);
        file.Close();
    }

}
