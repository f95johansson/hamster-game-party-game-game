﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour
{

    private static string userDataPath { get { return Application.persistentDataPath; } }

    public static GameControl Control;

    public Inventory Inventory = new Inventory();
    public PlayerData PlayerData = new PlayerData();
    public ShopData ShopData = new ShopData();


    private void Awake()
    {
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        if (Control == null)
        {
            DontDestroyOnLoad(gameObject);
            Control = this;
        }
        else if (Control != this)
        {
            Destroy(gameObject);
        }
    }

    //Berenice : not sure about that yet
    public void LoadInventory()
    {
        if (File.Exists(userDataPath + "/Inventory.dat"))
        {
            var bf = new BinaryFormatter();
            var file = File.Open(userDataPath + "/Inventory.dat", FileMode.Open);
            var data = (Inventory)bf.Deserialize(file);
            file.Close();

            Inventory.foodAmount = data.foodAmount;
            Inventory.moneyAmount = data.moneyAmount;
            Inventory.hamsterStates = data.hamsterStates;
        }
        else
        {
            //inventory.foodAmount = 0;
            //inventory.moneyAmount = 100;
        }
    }

    //Berenice : not sure about that yet
    public void SaveInventory()
    {
        var bf = new BinaryFormatter();
        var file = File.Create(userDataPath + "/Inventory.dat");

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

    public void LoadPlayerData()
    {
        var bf = new BinaryFormatter();
        if (File.Exists(userDataPath + "/PlayerData.dat"))
        {
            var file = File.Open(userDataPath + "/PlayerData.dat", FileMode.Open);
            var data = (PlayerData)bf.Deserialize(file);
            file.Close();

            PlayerData.numberCarrotsAllowed = data.numberCarrotsAllowed;
            PlayerData.numberCatsAllowed = data.numberCatsAllowed;
            //reset state of numberCarrotsAllowed
            //PlayerData.numberCarrotsAllowed = 3;
            //reset state of numberCatsAllowed
            //PlayerData.numberCatsAllowed = 3;
        }
    }

    public void SavePlayerData()
    {
        var bf = new BinaryFormatter();
        var file = File.Create(userDataPath + "/PlayerData.dat");

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

    public void LoadShopData()
    {
        var bf = new BinaryFormatter();
        if (File.Exists(userDataPath + "/ShopData.dat"))
        {
            var file = File.Open(userDataPath + "/ShopData.dat", FileMode.Open);
            var data = (ShopData)bf.Deserialize(file);
            file.Close();

            ShopData.currentEpochTime = data.currentEpochTime;
            ShopData.hamsterStatesShop = data.hamsterStatesShop;
            ShopData.ownHamster = data.ownHamster;
            //PlayerData.numberCarrotsAllowed = data.numberCarrotsAllowed;
            //PlayerData.numberCatsAllowed = data.numberCatsAllowed;
            //reset state of numberCarrotsAllowed
            //PlayerData.numberCarrotsAllowed = 3;
            //reset state of numberCatsAllowed
            //PlayerData.numberCatsAllowed = 3;
        }
    }

    public void SaveShopData()
    {
        var bf = new BinaryFormatter();
        var file = File.Create(userDataPath + "/ShopData.dat");

        //playerData data = new playerData();
        var data = new ShopData
        {
            currentEpochTime = ShopData.currentEpochTime,
            hamsterStatesShop = ShopData.hamsterStatesShop,
            ownHamster = ShopData.ownHamster

        };
            
        
    //data.inventory = inventory;

    bf.Serialize(file, data);
        file.Close();
    }

}
