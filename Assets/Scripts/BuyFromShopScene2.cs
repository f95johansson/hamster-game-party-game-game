using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuyFromShopScene2 : MonoBehaviour
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

    private uint[] cost = new uint[] { 100, 100, 100, 100, 100 };


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

    private void Start()
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
        SceneManager.LoadScene("LevelSelect");
    }

    private void PreviousShopScene()
    {
        SceneManager.LoadScene("HamsterShop");
    }

    private void ChangeScene(string scene)
    {
        if (scene != SceneManager.GetActiveScene().name)
        {
            //SceneManager
        }
    }

    private void TaskWithParameters(int buttonId)
    {
        switch (buttonId)
        {
            case 0:
                buyHamster(buttonId);
                break;
            case 1:
                buyHamster(buttonId);
                break;
            case 2:
                buyHamster(buttonId);
                break;
            case 3:
                buyHamster(buttonId);
                break;
            case 4:
                buyHamster(buttonId);
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
                inventorystat.moneyAmount -= cost[id];
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
    
    public static string getRandomName()
    {
        var index = (uint) Random.Range(0, names.Length);
        return names[index];
    }

    private static readonly string[] names = 
        {"Ace"
        ,"Acey"
        ,"Abrico"
        ,"Alfy"
        ,"Archie"
        ,"Argola"
        ,"Bram"
        ,"Bruno"
        ,"Bubba"
        ,"Buddy"
        ,"Buzz"
        ,"Caesar"
        ,"Chuck"
        ,"Cirrus"
        ,"Cooper"
        ,"Cotton"
        ,"Dexter"
        ,"Dion"
        ,"Ditto"
        ,"Dots"
        ,"Elmo"
        ,"Gus"
        ,"Hairy"
        ,"Happy"
        ,"Jasper"
        ,"Jo Jo"
        ,"Larry"
        ,"Lucius"
        ,"Mindy"
        ,"Muffy"
        ,"Mugs"
        ,"Pablo"
        ,"Pepper"
        ,"Rex"
        ,"Ripley"
        ,"Taz"
        ,"Teddy"
        ,"Tiny"
        ,"Aggy"
        ,"Amber"
        ,"Amelia"
        ,"Agnus"
        ,"Annie"
        ,"Apple"
        ,"April"
        ,"Ashes"
        ,"Ashley"
        ,"Babs"
        ,"Beans"
        ,"Bella"
        ,"Bertha"
        ,"Bijou"
        ,"Bitz"
        ,"Blitz"
        ,"Bonnie"
        ,"Boots"
        ,"Boress"
        ,"Candis"
        ,"Catnip"
        ,"Charm"
        ,"Cheeks"
        ,"Cheska"
        ,"Chili"
        ,"Chuu"
        ,"Conrad"
        ,"Cookie"
        ,"Curd"
        ,"Dolly"
        ,"Erma"
        ,"Foo  Foo"
        ,"Ginger"
        ,"Gretel"
        ,"Holly"
        ,"Honey"
        ,"Lady"
        ,"Lily"
        ,"Loulou"
        ,"Maggy"
        ,"Mini"
        ,"Minnie"
        ,"Pearl"
        ,"Windie"
        ,"Ally"
        ,"Axel"
        ,"Baby"
        ,"Bilbo"
        ,"Buffy"
        ,"Buster"
        ,"Button"
        ,"Cheeky"
        ,"Chewy"
        ,"Chip"
        ,"Chubby"
        ,"Cindy"
        ,"Disco"
        ,"Domino"
        ,"Ebi"
        ,"Elvis"
        ,"Emeril"
        ,"Flick"
        ,"Fluffy"
        ,"Hamlet"
        ,"Hammy"
        ,"Hank"
        ,"Henry"
        ,"Herman"
        ,"Jojo"
        ,"Karma"
        ,"Kernel"
        ,"Kitkat"
        ,"Kiwi"
        ,"Kobe"
        ,"Kujo"
        ,"Latte"
        ,"Lilly"
        ,"Lucky"
        ,"Marble"
        ,"Mimi"
        ,"Missy"
        ,"Mocha"
        ,"Muffin"
        ,"Nemo"
        ,"Niblet"
        ,"Nugget"
        ,"Odie"
        ,"Olly"
        ,"Oreo"
        ,"Panda"
        ,"Pauly"
        ,"Pedro"
        ,"Perogy"
        ,"Pooky"
        ,"Ringo"
        ,"Rocky"
        ,"Shaggy"
        ,"Shrimp"
        ,"Skippy"
        ,"Sleepy"
        ,"Sparky"
        ,"Stitch"
        ,"Taco"
        ,"Tot"
        ,"Tippy"
        ,"Trixie"
        ,"Tofu"
        ,"Toffee"
        ,"Turbo"
        ,"Uni"
        ,"Ziggy"
        ,"Zippy"
        ,"Boo  Bear"
        ,"Bunny"
        ,"Cocoa"
        ,"Dale"
        ,"Desert"
        ,"Echo"
        ,"Fu-Fu"
        ,"Fufu"
        ,"Furry"
        ,"Fuzzy"
        ,"Gem"
        ,"Gin"
        ,"Guava"
        ,"Gummie"
        ,"Hazel"
        ,"Mickey"
        ,"Mojo"
        ,"Mouse"
        ,"Paws"
        ,"Peewee"
        ,"Pepe"
        ,"Powder"
        ,"Sweety"
        ,"Abster"
        ,"Alfie"
        ,"Axe"
        ,"Bigon"
        ,"Butter"
    };

}
