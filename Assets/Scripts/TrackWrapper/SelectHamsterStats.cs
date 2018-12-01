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
		_hamsterStats = new List<HamsterStats>();
		for (var i = 0; i < 6; i++)
		{
			var nue = Instantiate(HS);
			nue.Index = (uint) i;
			nue.transform.SetParent(transform, false);
			nue.transform.localPosition = PositionFromIndex(i);
			_hamsterStats.Add(nue);
			
			nue.OnSelected.AddListener(() =>
			{
				var canvas = GetComponent<CanvasGroup>();
				canvas.interactable = false;
				canvas.alpha = 0;
				canvas.blocksRaycasts = false;
			});
		}
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

	private void Update()
	{
		foreach (var nue in _hamsterStats)
		{
			nue.transform.localPosition = PositionFromIndex((int) nue.Index);
		}
	}
}
