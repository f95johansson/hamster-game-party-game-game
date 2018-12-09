using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarSystem : MonoBehaviour
{
    private WinCondition _winCondition;
    public Image GoButton;

    private Text _goal;
    private Text _status;

    private void Start()
    {
        var texts = GetComponentsInChildren<Text>();
        _goal = texts[0];
        _status = texts[1];

        _winCondition = FindObjectOfType<WinCondition>();
        
        _goal.text = _winCondition.Description();
        _status.text = _winCondition.ChangedState();

        _winCondition.OnWin().AddListener(() =>
        {
            var levelName = SceneManager.GetActiveScene().name;
            var progress = GameControl.Control.Progress;
            var inventory = GameControl.Control.Inventory;

            if (!progress.HasCleared(levelName))
            {
                inventory.AddMoney(50);
                GameControl.Control.SaveInventory();
            }

            var selectHamsterState = FindObjectOfType<SelectHamsterStats>();

            if (selectHamsterState != null) // on intro levels
            {
                var currentHamsterId = selectHamsterState.CurrentHamsterID;

                if (currentHamsterId != null)
                {
                    foreach (var inventoryHamster in inventory.hamsterStates)
                    {
                        if (inventoryHamster.getUUID() == currentHamsterId)
                        {
                            inventoryHamster.DecreaseFoodLevel(2);
                        }
                    }
                }
            }
            
            GameControl.Control.SaveInventory();
            progress.SaveTrackProgress(levelName, true, true, true);
            FindObjectOfType<WinInformation>().Show();
        });
        
        _winCondition.OnStateChange().AddListener(() => { _status.text = _winCondition.ChangedState(); });

        _winCondition.OnTestWin().AddListener(() => { 
            var buttonColor = GoButton.color;
            var blinkColor = new Color(99/255f, 224/255f, 65/255f);
            StartCoroutine(Blink(GoButton, .3f, buttonColor, blinkColor, () => {
                StartCoroutine(Blink(GoButton, .3f, buttonColor, blinkColor, () => {
                    StartCoroutine(DelayedBlink(1, GoButton, .3f, buttonColor, blinkColor, () => {
                        StartCoroutine(Blink(GoButton, .3f, buttonColor, blinkColor, () => {}));    
                    }));
                }));
            }));
        });

        
    }

    private IEnumerator DelayedBlink(float waitTime, Graphic graphic, float time, Color color1, Color color2, Action after) {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(Blink(graphic, time, color1, color2, after));
    }

    private static IEnumerator Blink(Graphic graphic, float time, Color color1, Color color2, Action after)
    {
        var timeLeft = time/2;
        graphic.color = color1;
        while (timeLeft > 0) {
            timeLeft -= Time.fixedDeltaTime;
            graphic.color = Color.Lerp(color1, color2, 1-timeLeft/(time/2));
            yield return new WaitForFixedUpdate();
        }
        graphic.color = color2;
        timeLeft = time/2;
        while (timeLeft > 0) {
            timeLeft -= Time.fixedDeltaTime;
            graphic.color = Color.Lerp(color2, color1, 1-timeLeft/time);
            yield return new WaitForFixedUpdate();
        }
        graphic.color = color1;
        
        after();
        yield return null;
    }
}
