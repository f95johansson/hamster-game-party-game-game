using UnityEngine;

//The glue between the track and the track-wrapper
public class AddWrapper : MonoBehaviour
{
	public string LevelName = "unnamed";
	public WinCondition WinCondition;
	
	private void Awake()
	{
		Navigation.AddWrapper();
	}

	private void Start()
	{
		FindObjectOfType<FadeOut>().SetName(LevelName);
		Destroy(gameObject);
		var starSystem = FindObjectOfType<StarSystem>();
		starSystem.WinCondition = WinCondition;
	}
}
