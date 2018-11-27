using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HamsterController : MonoBehaviour {

    public Camera camera;
    public GameObject hamster;
    public Text foodAmountText;
    public Button exitButton;

    private float maxWidth;

    //PERSISTENCE
    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        GameControl.Control.LoadInventory();
        GameControl.Control.LoadPlayerData();

    }

    private void OnDestroy()
    {
        GameControl.Control.SaveInventory();
        GameControl.Control.SavePlayerData();
    }

    // Use this for initialization
    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0);
        Vector3 targetWidth = camera.ScreenToWorldPoint(upperCorner);
        float hamsterWidth = hamster.GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - hamsterWidth;
        exitButton.onClick.AddListener(ExitScene);

        UpdateFoodText();
        
        SpawnHamsters();
    }

    public void FixedUpdate()
    {
        UpdateFoodText();
    }

    void ExitScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void SpawnHamsters()
    {
        uint i = 0;
        while ((i<GameControl.Control.Inventory.hamsterStates.Length) && (GameControl.Control.Inventory.hamsterStates[i]!=null))
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-maxWidth, maxWidth),
                transform.position.y,
                transform.position.z);
            GameObject hamsterInScene = Instantiate(hamster, spawnPosition, Quaternion.identity);
            

            hamsterInScene.GetComponent<HamsterPrefab>().setIndex(i);
            
            hamsterInScene.GetComponent<HamsterPrefab>().UpdateScaleWeight();

            i++;
        }
       
        
    }

    public void UpdateFoodText()
    {
        foodAmountText.text = (GameControl.Control.Inventory.foodAmount).ToString();
    }

   

}
