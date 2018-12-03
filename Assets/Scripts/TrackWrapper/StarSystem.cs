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
            GameControl.Control.Progress.SaveTrackProgress(SceneManager.GetActiveScene().name, true, true, true);
            FindObjectOfType<WinInformation>().Show();
        });
        
        _winCondition.OnStateChange().AddListener(() => { _status.text = _winCondition.ChangedState(); });

        _winCondition.OnTestWin().AddListener(() => { 
            Color buttonColor = GoButton.color;
            Color blinkColor = new Color(99/255f, 224/255f, 65/255f);
            StartCoroutine(blink(GoButton, .3f, buttonColor, blinkColor, () => {
                StartCoroutine(blink(GoButton, .3f, buttonColor, blinkColor, () => {
                    StartCoroutine(delayedBlink(1, GoButton, .3f, buttonColor, blinkColor, () => {
                        StartCoroutine(blink(GoButton, .3f, buttonColor, blinkColor, () => {}));    
                    }));
                }));
            }));
        });

        
    }

    private IEnumerator delayedBlink(float waitTime, Image image, float time, Color color1, Color color2, Action after) {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(blink(image, time, color1, color2, after));
    }

    private IEnumerator blink(Image image, float time, Color color1, Color color2, Action after)
    {
        var timeLeft = time/2;
        image.color = color1;
        while (timeLeft > 0) {
            timeLeft -= Time.fixedDeltaTime;
            image.color = Color.Lerp(color1, color2, 1-timeLeft/(time/2));
            yield return new WaitForFixedUpdate();
        }
        image.color = color2;
        timeLeft = time/2;
        while (timeLeft > 0) {
            timeLeft -= Time.fixedDeltaTime;
            image.color = Color.Lerp(color2, color1, 1-timeLeft/time);
            yield return new WaitForFixedUpdate();
        }
        image.color = color1;
        
        after();
        yield return null;
    }
}
