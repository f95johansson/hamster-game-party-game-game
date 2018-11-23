using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buyFromShopScene2 : MonoBehaviour {

    public Button[] m_Button;
    public Text[] m_TextCost;
    public Button m_NewShopPage, m_PreviusScene, m_exit;
    public Text M_TextMoney;
    public AllTheBars[] HamsterBars;
    public Sprite[] BarSprite;

    private int[] cost = new int[] { 10, 10, 10, 10, 10 };

    //private string[] items = new string[] { "Food", "", "", "", "" };
    // Use this for initialization
    private uint money;

    private void Awake()
    {
        GameControl.Control.LoadInventory();
        GameControl.Control.LoadPlayerData();
        GameControl.Control.LoadShopData();
        updateMoneyText();
        m_NewShopPage.interactable = false;
        m_NewShopPage.GetComponent<CanvasGroup>().alpha = 0.5f;
        GameControl.Control.ShopData.CheckTime();
        SetStateOfButton();
    }

    private void OnDestroy()
    {
        GameControl.Control.SaveInventory();
        GameControl.Control.SavePlayerData();
        GameControl.Control.SaveShopData();
    }

    void Start () {
        
        for (int i = 0; i < GameControl.Control.ShopData.hamsterStatesShop.Length; i++)
        {
            m_Button[i].onClick.AddListener(delegate { TaskWithParameters(i); });
            var state = GameControl.Control.ShopData.hamsterStatesShop[i];
            var bars = HamsterBars[i];
            bars.Weight().SetNumber(BarSprite[state.WeightLevel]);
            bars.Speed().SetNumber(BarSprite[state.SpeedLevel]);
            bars.Friction().SetNumber(BarSprite[state.FrictionLevel]);
            bars.TurnSpeed().SetNumber(BarSprite[state.TurnSpeedLevel]);
        }
        m_exit.onClick.AddListener(ExitScene);
        m_PreviusScene.onClick.AddListener(PreviousShopScene);
    }
	
	// Update is called once per frame
	void Update () {
        if(money != GameControl.Control.Inventory.moneyAmount) {
            money = GameControl.Control.Inventory.moneyAmount;
            updateMoneyText();
        }

	}

    void ExitScene() {
        //SceneManager.LoadScene("Scenes/HamsterShopScene2", LoadSceneMode.Additive);
    }

    void PreviousShopScene()
    {
        //SceneManager.LoadScene(2);
        SceneManager.LoadScene("HamsterShop");
    }

    void ChangeScene(string scene){
        if (scene != SceneManager.GetActiveScene().name) {
            //SceneManager
        }
    }

    void TaskWithParameters(int ButtonId)
    {
        uint item;
        uint costForItem;
        switch(ButtonId) {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            default:

                break;
        }
    }

    void updateMoneyText() {
        M_TextMoney.text = "The Money you have: " + GameControl.Control.Inventory.moneyAmount;
    }

    void SetStateOfButton() {
        for (int i = 0; i < GameControl.Control.ShopData.ownHamster.Length; i++) {
            m_TextCost[i].text = "" + cost[i];
            if (GameControl.Control.ShopData.ownHamster[i] == 1) {
                m_Button[i].interactable = false;
                m_Button[i].GetComponent<CanvasGroup>().alpha = 0.5f;
            }
        }

    }
}
