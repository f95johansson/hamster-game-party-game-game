using UnityEngine;
using UnityEngine.UI;

public class StarSystem : MonoBehaviour
{
    public WinCondition WinCondition;
    public Text LevelName;

    private Text _goal;
    private Text _status;


    private void Start()
    {
        var texts = GetComponentsInChildren<Text>();
        _goal = texts[0];
        _status = texts[1];
        
        _goal.text = WinCondition.Description();
        _status.text = WinCondition.ChangedState();

        WinCondition.OnWin().AddListener(() =>
        {
            GameControl.Control.Progress.SaveTrackProgress(LevelName.text, true, true, true);
            FindObjectOfType<WinInformation>().Show();
        });
        
        WinCondition.OnStateChange().AddListener(() => { _status.text = WinCondition.ChangedState(); });
    }
}
