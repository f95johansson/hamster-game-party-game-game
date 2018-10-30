using UnityEngine;
using UnityEngine.Events;

public class Handle : MonoBehaviour {
	public UnityEvent OnChange = new UnityEvent();

	public void Update()
	{
		if (transform.hasChanged)
		{
			OnChange.Invoke();
		}
	}
}
