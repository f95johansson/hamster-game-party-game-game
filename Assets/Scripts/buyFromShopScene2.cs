using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buyFromShopScene2 : MonoBehaviour
{

    public Button[] m_Button;
    public Text[] m_TextCost;
    public Text[] m_HamsterName;
    public Button m_NewShopPage, m_PreviusScene, m_exit;
    public Text M_TextMoney;
    public AllTheBars[] HamsterBars;
    public Sprite[] BarSprite;

    private ShopData shopstat;
    private Inventory inventorystat;

    private int[] cost = new int[] { 100, 100, 100, 100, 100 };


    //private string[] items = new string[] { "Food", "", "", "", "" };
    // Use this for initialization
    private uint money;

    private void Awake()
    {
        GameControl.Control.LoadInventory();
        GameControl.Control.LoadPlayerData();
        GameControl.Control.LoadShopData();
        shopstat = GameControl.Control.ShopData;
        inventorystat = GameControl.Control.Inventory;
        updateMoneyText();
        m_NewShopPage.interactable = false;
        m_NewShopPage.GetComponent<CanvasGroup>().alpha = 0.5f;
        shopstat.CheckTime();
        SetStateOfButton();
    }

    private void OnDestroy()
    {
        GameControl.Control.SaveInventory();
        GameControl.Control.SavePlayerData();
        GameControl.Control.SaveShopData();
    }

    void Start()
    {

        for (int i = 0; i < GameControl.Control.ShopData.hamsterStatesShop.Length; i++)
        {
            var state = shopstat.hamsterStatesShop[i];
            var bars = HamsterBars[i];
            bars.Weight().SetNumber(BarSprite[state.WeightLevel]);
            bars.Speed().SetNumber(BarSprite[state.SpeedLevel]);
            bars.Friction().SetNumber(BarSprite[state.FrictionLevel]);
            bars.TurnSpeed().SetNumber(BarSprite[state.TurnSpeedLevel]);
        }
        m_Button[0].onClick.AddListener(delegate { TaskWithParameters(0); });
        m_Button[1].onClick.AddListener(delegate { TaskWithParameters(1); });
        m_Button[2].onClick.AddListener(delegate { TaskWithParameters(2); });
        m_Button[3].onClick.AddListener(delegate { TaskWithParameters(3); });
        m_Button[4].onClick.AddListener(delegate { TaskWithParameters(4); });
        m_exit.onClick.AddListener(ExitScene);
        m_PreviusScene.onClick.AddListener(PreviousShopScene);
    }

    // Update is called once per frame
    void Update()
    {
        if (money != inventorystat.moneyAmount)
        {
            money = inventorystat.moneyAmount;
            updateMoneyText();
        }
        SetStateOfButton();

    }

    private void ExitScene()
    {
        //SceneManager.LoadScene("Scenes/HamsterShopScene2", LoadSceneMode.Additive);
        SceneManager.LoadScene("LevelSelect");
    }

    private void PreviousShopScene()
    {
        //SceneManager.LoadScene(2);
        SceneManager.LoadScene("HamsterShop");
    }

    private void ChangeScene(string scene)
    {
        if (scene != SceneManager.GetActiveScene().name)
        {
            //SceneManager
        }
    }

    private void TaskWithParameters(int ButtonId)
    {
        switch (ButtonId)
        {
            case 0:
                buyHamster(ButtonId);
                break;
            case 1:
                buyHamster(ButtonId);
                break;
            case 2:
                buyHamster(ButtonId);
                break;
            case 3:
                buyHamster(ButtonId);
                break;
            case 4:
                buyHamster(ButtonId);
                break;
            default:

                break;
        }
    }

    private void buyHamster(int id)
    {
        if (cost[id] <= inventorystat.moneyAmount)
        {
            if (inventorystat.HamsterOwns < 10 && shopstat.ownHamster[id] != 1)
            {
                shopstat.ownHamster[id] = 1;
                inventorystat.AddHamster(shopstat.hamsterStatesShop[id]);
            }

        }
    }

    private void updateMoneyText()
    {
        M_TextMoney.text = "Money: " + GameControl.Control.Inventory.moneyAmount;
    }

    private void SetStateOfButton()
    {
        for (int i = 0; i < GameControl.Control.ShopData.ownHamster.Length; i++)
        {
            m_TextCost[i].text = "" + cost[i];
            m_HamsterName[i].text = GameControl.Control.ShopData.hamsterStatesShop[i].HamsterName;
            if (GameControl.Control.ShopData.ownHamster[i] == 1)
            {
                m_Button[i].interactable = false;
                m_Button[i].GetComponent<CanvasGroup>().alpha = 0.5f;
            }
        }

    }

}
