
using UnityEngine;
using UnityEngine.Events;

public abstract class WinCondition : MonoBehaviour
{
    public abstract UnityEvent OnWin();
    public abstract UnityEvent OnStateChange();
    public abstract string Description();
    public abstract string ChangedState();
}
