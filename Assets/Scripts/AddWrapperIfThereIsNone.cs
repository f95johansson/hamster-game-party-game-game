using UnityEngine;

public class AddWrapperIfThereIsNone : MonoBehaviour {

	private void Start ()
	{
		var effectorHolder = FindObjectOfType<EffectorHolder>();

		if (!effectorHolder) // this means we have no track wrapper
		{
			Navigation.AddWrapper();
		}
		
		Destroy(gameObject);
	}
}
