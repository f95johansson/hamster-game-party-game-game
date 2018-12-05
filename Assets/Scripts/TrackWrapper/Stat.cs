using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
	public uint Points;
	public Sprite[] ListOfSprites = new Sprite[6];
	private Image _statImage;
	private uint _prevPoints; 
	
	private void Start ()
	{
		_statImage = GetComponentInChildren<Image>();
		
		OnChange();
	}

	private void OnChange()
	{
		_statImage.sprite = ListOfSprites[Points];
		_prevPoints = Points;
	}

	private void Update()
	{
		if (_prevPoints != Points)
		{
			OnChange();
		}
	}
}
