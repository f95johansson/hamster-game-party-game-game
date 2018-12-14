﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buyFromShop : MonoBehaviour {

    public Button m_Button_1, m_Button_2, m_Button_3, m_exit, m_PreviusScene, m_NewShopPage;
    public Text m_Text_1, m_Text_2, m_Text_3;
    public Text m_TextCost_1, m_TextCost_2, m_TextCost_3;
    public Text M_TextMoney;
    //private string[] items = new string[] { "Food", "", "", "", "" };
    private uint[] cost = new uint[] { 10, 100, 100 };
    private int[] Max = new int[] { 100, 5, 5};
    // Use this for initialization
    private uint money;

    private void Awake()
    {
        GameControl.Control.LoadInventory();
        GameControl.Control.LoadPlayerData();
        updatePriceOfItem();
        updateMoneyText();
        SetStateOfButton();
        m_PreviusScene.interactable = false;
        m_PreviusScene.GetComponent<CanvasGroup>().alpha = 0.5f;
    }

    private void OnDestroy()
    {
        GameControl.Control.SaveInventory();
        GameControl.Control.SavePlayerData();
    }

    private void Start () {
        m_Button_1.onClick.AddListener(delegate { TaskWithParameters(0); });
        m_Button_2.onClick.AddListener(delegate { TaskWithParameters(1); });
        m_Button_3.onClick.AddListener(delegate { TaskWithParameters(2); });
        m_exit.onClick.AddListener(ExitScene);
        m_NewShopPage.onClick.AddListener(NextShopScene);
    }

    // Update is called once per frame
    private void Update () {
        if(money != GameControl.Control.Inventory.moneyAmount) {
            money = GameControl.Control.Inventory.moneyAmount;
            updateMoneyText();
            updatePriceOfItem();
        }
       
	}

    private void ExitScene() {
        SceneManager.LoadScene("LevelSelect");
    }

    private void NextShopScene()
    {
        //SceneManager.LoadScene(2);
        SceneManager.LoadScene("HamsterShopScene2");
    }

    private void ChangeScene(string scene){
        if (scene != SceneManager.GetActiveScene().name) {
            //SceneManager
        }
    }

    private void TaskWithParameters(int ButtonId)
    {
        uint item;
        uint costForItem;
        switch(ButtonId) {
            case 0:
                item = GameControl.Control.Inventory.foodAmount;
                if (cost[ButtonId] <= money) {
                    if (item < Max[ButtonId])
                    {
                        GameControl.Control.Inventory.moneyAmount = money - cost[ButtonId];
                        GameControl.Control.Inventory.foodAmount = item + 1;
                    }
                    SetStateOfButton();
                }

                break;
            case 1:
                item = GameControl.Control.PlayerData.numberCarrotsAllowed;
                costForItem = cost[ButtonId];
                if (costForItem <= money)
                {
                    if (item < Max[ButtonId])
                    {
                        item += 1;
                        GameControl.Control.Inventory.moneyAmount -= costForItem;
                        GameControl.Control.PlayerData.numberCarrotsAllowed = item;
                        cost[ButtonId] = 150;
                    }
                    SetStateOfButton();
                }
                break;
            case 2:
                item = GameControl.Control.PlayerData.numberCatsAllowed;
                costForItem = cost[ButtonId];
                if (costForItem <= money)
                {
                    if (item < Max[ButtonId])
                    {
                        item += 1;
                        GameControl.Control.Inventory.moneyAmount -= costForItem;
                        GameControl.Control.PlayerData.numberCatsAllowed = item;
                        cost[ButtonId] = 150;
                    }
                    SetStateOfButton();
                }
                break;
            default:

                break;
        }
    }

    private void updateMoneyText() {
        M_TextMoney.text = "Money: " + GameControl.Control.Inventory.moneyAmount;
    }

    private void updatePriceOfItem() {
        m_Text_1.text = "You own: " + (GameControl.Control.Inventory.foodAmount).ToString();
        m_TextCost_1.text = "" + cost[0];
        m_Text_2.text = "Your max: " + (GameControl.Control.PlayerData.numberCarrotsAllowed).ToString();
        m_TextCost_2.text = "" + cost[1];
        m_Text_3.text = "Your max: " + (GameControl.Control.PlayerData.numberCatsAllowed).ToString();
        m_TextCost_3.text = "" + cost[2];
        if (GameControl.Control.PlayerData.numberCarrotsAllowed == 4) {
            cost[1] = 150;
        }
        if (GameControl.Control.PlayerData.numberCatsAllowed == 4)
        {
            cost[2] = 150;
        }
    }

    private void SetStateOfButton() {
        if (GameControl.Control.Inventory.foodAmount >= Max[0])
        {
            m_Button_1.interactable = false;
            m_Button_1.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
        if (GameControl.Control.PlayerData.numberCarrotsAllowed >= Max[1])
        {
            m_Button_2.interactable = false;
            m_Button_2.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
        if (GameControl.Control.PlayerData.numberCatsAllowed >= Max[2])
        {
            m_Button_3.interactable = false;
            m_Button_3.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
    }
}
