using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buyFromShopScene2 : MonoBehaviour {

    public Button m_Button_1, m_Button_2, m_Button_3, m_Button_4, m_Button_5, m_exit, m_PreviusScene, m_NewShopPage;
    public Text m_TextCost_1, m_TextCost_2, m_TextCost_3, m_TextCost_4, m_TextCost_5;
    public Text M_TextMoney;

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

    }

    private void OnDestroy()
    {
        GameControl.Control.SaveInventory();
        GameControl.Control.SavePlayerData();
        GameControl.Control.SaveShopData();
    }

    void Start () {
        m_Button_1.onClick.AddListener(delegate { TaskWithParameters(0); });
        m_Button_2.onClick.AddListener(delegate { TaskWithParameters(1); });
        m_Button_3.onClick.AddListener(delegate { TaskWithParameters(2); });
        m_Button_4.onClick.AddListener(delegate { TaskWithParameters(3); });
        m_Button_5.onClick.AddListener(delegate { TaskWithParameters(4); });
        m_exit.onClick.AddListener(ExitScene);
        m_PreviusScene.onClick.AddListener(PreviusShopScene);
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

    void PreviusShopScene()
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

    }
}
