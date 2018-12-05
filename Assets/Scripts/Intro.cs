using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
	public Button Fast;
	public Button Agile;
	public Button Thoughtful;
	private Inventory _inventory;

	public CanvasGroup Buttons;
	public CanvasGroup Result;

	public Text ResultText;

	private const string LevelOne = "IntroTrack";
	private const string LevelTwo = "IntroTrack2";

	private void Start ()
	{
		GameControl.Control.LoadInventory();
		if (GameControl.Control.Inventory.HasAHamster())
		{
			var progress = GameControl.Control.Progress;

			if (progress.HasCleared(LevelTwo))
			{
				Navigation.StartLevelSelect();	
			}
			else
			{
				var goTo = (progress.HasCleared(LevelOne)) ? LevelTwo : LevelOne;
				Navigation.StartTrack(goTo);
			}
			return;
		}
		
		Buttons.alpha = 1;
		Buttons.interactable = true;
		Buttons.blocksRaycasts = true;
		
		Result.alpha = 0;
		Result.interactable = false;
		Result.blocksRaycasts = false;
		
		_inventory = GameControl.Control.Inventory;

		Add(Thoughtful, 3, 1, 2, 2);
		Add(Fast, 2, 3, 2, 3);
		Add(Agile, 2, 2, 1, 3);
	}

	public void Add(Button b, uint friction, uint speed, uint weight, uint turnSpeed)
	{
		b.onClick.AddListener(() =>
		{
			var hamster = new HamsterState
			{
				FrictionLevel = friction,
				SpeedLevel = speed,
				WeightLevel = weight,
				TurnSpeedLevel = turnSpeed,
				HamsterName = BuyFromShopScene2.getRandomName(),
				foodLevel = 5
			};

			_inventory.AddHamster(hamster);
			GameControl.Control.SaveInventory();
			Buttons.alpha = 0;
			Buttons.blocksRaycasts = false;
			Buttons.interactable = false;

			Result.alpha = 1;
			Result.blocksRaycasts = true;
			Result.interactable = true;

			ResultText.text += " " + hamster.HamsterName + "!";
		});
	}
}
