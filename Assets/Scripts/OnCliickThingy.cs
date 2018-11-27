using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCliickThingy : MonoBehaviour {

    public readonly UnityEvent MouseEnter = new UnityEvent();

    public readonly UnityEvent MouseExit = new UnityEvent();

    public readonly UnityEvent MouseClick = new UnityEvent();


    private void OnMouseUpAsButton()
    {
        MouseClick.Invoke();
    }

    private void OnMouseEnter()
    {
        MouseEnter.Invoke();
    }

    private void OnMouseExit()
    {
        MouseExit.Invoke();
    }
}
