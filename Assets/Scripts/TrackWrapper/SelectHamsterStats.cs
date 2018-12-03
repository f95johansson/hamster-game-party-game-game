using System.Collections.Generic;
using UnityEngine;

public class SelectHamsterStats : MonoBehaviour
{
	public uint NrPerRow = 3;
	public HamsterStats HS;
	public Vector3 StartPos;

	public float OffX;
	public float OffY;

	private List<HamsterStats> _hamsterStats;

	private void Start ()
	{		
		GameControl.Control.LoadInventory();
		_hamsterStats = new List<HamsterStats>();

		for (var i = 0; i < 6; i++)
		{
			var state = GameControl.Control.Inventory.hamsterStates[i];

			if (state != null)
			{
				var nue = Instantiate(HS);
				nue.SetState(state);
				nue.transform.SetParent(transform, false);
				nue.transform.localPosition = PositionFromIndex(i);
				_hamsterStats.Add(nue);
			
				nue.OnSelected.AddListener(() =>
				{
					var canvas = GetComponent<CanvasGroup>();
					canvas.interactable = false;
					canvas.alpha = 0;
					canvas.blocksRaycasts = false;
					Select(nue);
				});
			} 
		}

		if (_hamsterStats.Count > 0)
		{
			Select(_hamsterStats[0]);
		}
		else
		{
			Debug.Log("Does not have hamster, we should do something here");
		}
	}

	private void Select(HamsterStats hamster)
	{
		FindObjectOfType<HamsterStart>().NewStats(hamster.SpeedPoints, hamster.WeightPoints, hamster.TurnSpeedPoints, hamster.FrictionPoints);
	}

	private Vector3 PositionFromIndex(int i)
	{
		var x = i < NrPerRow ? i : i-NrPerRow;
		var y = i < NrPerRow ? 0 : 1;
		
		return StartPos + new Vector3(x * OffX, y * OffY, HS.transform.position.z) - LeftCorner();
	}

	private Vector3 LeftCorner()
	{
		var width = (NrPerRow - 1) * OffX;
		var height = OffY;
		return new Vector3(width/2, height/2, 0);
	}
}
