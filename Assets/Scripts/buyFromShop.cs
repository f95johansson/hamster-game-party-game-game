using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buyFromShop : MonoBehaviour {

    public Button m_Button_1, m_Button_2, m_Button_3, m_Button_4, m_Button_5;
    public Text m_Text_1, m_Text_2, m_Text_3, m_Text_4, m_Text_5;
    //private string[] items = new string[] { "Food", "", "", "", "" };
    private int[] cost = new int[] {10,1000,1000,1000,1000};
    private int[] Max = new int[] { 100, 100, 100, 100, 100 };
    // Use this for initialization

    private void Awake()
    {
        GameControl.control.loadInventory();
    }

    private void OnDestroy()
    {
        GameControl.control.saveInventory();
    }

    void Start () {
        m_Text_1.text = "" + GameControl.control.Food;
        m_Text_2.text = "" + GameControl.control.Food;
        m_Text_3.text = "" + GameControl.control.Food;
        m_Text_4.text = "" + GameControl.control.Food;
        m_Text_5.text = "" + GameControl.control.Food;
        m_Button_1.onClick.AddListener(delegate { TaskWithParameters(0); });
        m_Button_2.onClick.AddListener(delegate { TaskWithParameters(1); });
        m_Button_3.onClick.AddListener(delegate { TaskWithParameters(2); });
        m_Button_4.onClick.AddListener(delegate { TaskWithParameters(3); });
        m_Button_5.onClick.AddListener(delegate { TaskWithParameters(4); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void TaskWithParameters(int ButtonId)
    {
        int item;
        int money;
        switch(ButtonId) {
            case 0:
                item = GameControl.control.Food;
                money = GameControl.control.Money;
                if (cost[ButtonId] < money) {
                    if (item < Max[ButtonId])
                    {
                        GameControl.control.Money = money - cost[ButtonId];
                        GameControl.control.Food = item + 1;
                        m_Text_1.text = (item + 1).ToString();
                    }
                }
                break;
            case 1:
                item = GameControl.control.Food;
                money = GameControl.control.Money;
                if (cost[ButtonId] < money)
                {
                    if (item < Max[ButtonId])
                    {
                        GameControl.control.Money = money - cost[ButtonId];
                        GameControl.control.Food = item + 1;
                        m_Text_2.text = (item + 1).ToString();
                    }
                }
                break;
            case 2:
                item = GameControl.control.Food;
                money = GameControl.control.Money;
                if (cost[ButtonId] < money)
                {
                    if (item < Max[ButtonId])
                    {
                        GameControl.control.Money = money - cost[ButtonId];
                        GameControl.control.Food = item + 1;
                        m_Text_3.text = (item + 1).ToString();
                    }
                }
                break;
            case 3:
                item = GameControl.control.Food;
                money = GameControl.control.Money;
                if (cost[ButtonId] < money)
                {
                    if (item < Max[ButtonId])
                    {
                        GameControl.control.Money = money - cost[ButtonId];
                        GameControl.control.Food = item + 1;
                        m_Text_4.text = (item + 1).ToString();
                    }
                }
                break;
            case 4:
                item = GameControl.control.Food;
                money = GameControl.control.Money;
                if (cost[ButtonId] < money)
                {
                    if (item < Max[ButtonId])
                    {
                        GameControl.control.Money = money - cost[ButtonId];
                        GameControl.control.Food = item + 1;
                        m_Text_5.text = (item + 1).ToString();
                    }
                }
                break;
            default:

                break;
        }
    }
}
