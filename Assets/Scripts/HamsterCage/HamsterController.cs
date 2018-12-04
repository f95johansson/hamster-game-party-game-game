using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
        //Debug.Log("Save Inventory " + GameControl.Control.Inventory.hamsterStates[0].HamsterName);
        GameControl.Control.SavePlayerData();
    }

    // Use this for initialization
    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 10);
        Vector3 targetWidth = camera.ScreenToWorldPoint(upperCorner);
        float hamsterWidth = hamster.GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - hamsterWidth;

        exitButton.onClick.AddListener(ExitScene);

        
        UpdateFoodText();
        
        StartCoroutine(SpawnHamsters());
    }

    public void FixedUpdate()
    {
        UpdateFoodText();
    }

    void ExitScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    IEnumerator SpawnHamsters()
    {
        uint i = 0;
        for (i=0;  i<GameControl.Control.Inventory.hamsterStates.Length; i++)
        {
            if (GameControl.Control.Inventory.hamsterStates[i] != null && GameControl.Control.Inventory.hamsterStates[i].HamsterName != "")
            {
                Vector3 spawnPosition = new Vector3(
                Random.Range(-maxWidth + 2 * hamster.GetComponent<Renderer>().bounds.extents.x, maxWidth),
                transform.position.y,
                transform.position.z);
                GameObject hamsterInScene = Instantiate(hamster, spawnPosition, Quaternion.identity);


                hamsterInScene.GetComponent<HamsterPrefab>().setIndex(i);

                hamsterInScene.GetComponent<HamsterPrefab>().UpdateScaleWeight();
                yield return new WaitForSeconds(0.5f);
            }
            //Debug.Log("Checked hamsterStates");
        }
       
        
    }

    public void SpawnOneHamster(GameObject hamsterToGenerate)
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-maxWidth + 2 * hamster.GetComponent<Renderer>().bounds.extents.x, maxWidth),
            transform.position.y,
            transform.position.z);
        hamsterToGenerate.transform.position = spawnPosition;
        Debug.Log("SpawnOneHamster CALLED");
    }

    public void UpdateFoodText()
    {
        foodAmountText.text = (GameControl.Control.Inventory.foodAmount).ToString();
    }

   

}
