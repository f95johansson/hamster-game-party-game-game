using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HamsterController : MonoBehaviour {

    private Camera _camera;
    public HamsterPrefab Hamster;
    public Text foodAmountText;
    public Button exitButton;

    public SpriteRenderer Cage;

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

    private void Start()
    {
        _camera = Camera.main;
        Assert.IsNotNull(_camera);

        exitButton.onClick.AddListener(ExitScene);
        
        UpdateFoodText();
        
        StartCoroutine(SpawnHamsters());
    }

    private void Update()
    {
        var cageWidth = Cage.bounds.max.x - Cage.bounds.min.x;
        var camWidth = _camera.ViewportToWorldPoint(new Vector2(1, 0)).x -
                       _camera.ViewportToWorldPoint(new Vector2(0, 0)).x;

        _camera.orthographicSize *= cageWidth / camWidth;
    }

    public void FixedUpdate()
    {
        UpdateFoodText();
    }

    private static void ExitScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    private IEnumerator SpawnHamsters()
    {
        for (uint i=0; i<GameControl.Control.Inventory.hamsterStates.Length; i++)
        {
            if (GameControl.Control.Inventory.hamsterStates[i] != null && GameControl.Control.Inventory.hamsterStates[i].HamsterName != "")
            {
                var hamster = Instantiate(Hamster);
                SpawnOneHamster(hamster);
                hamster.GetComponent<HamsterPrefab>().SetIndex(i);
                hamster.GetComponent<HamsterPrefab>().UpdateScaleWeight();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public void SpawnOneHamster(HamsterPrefab hamster)
    {
        var hB = hamster.GetComponent<Renderer>().bounds;
        
        var leftBound = Cage.bounds.min.x;
        leftBound += 0.2f * Cage.bounds.size.x;
        leftBound += hB.center.x - hB.min.x;

        var rightBound = Cage.bounds.max.x;
        rightBound += hB.center.x - hB.max.x;
        
        var spawnPosition = new Vector3(
            Random.Range(leftBound, rightBound),
            transform.position.y,
            transform.position.z);
        hamster.transform.position = spawnPosition;
        Debug.Log("SpawnOneHamster CALLED");
    }

    public void UpdateFoodText()
    {
        foodAmountText.text = (GameControl.Control.Inventory.foodAmount).ToString();
    }
}
