using System.Collections;
using System.Linq;
using UnityEngine;

public class InCaseNoHamster : MonoBehaviour {

	private void Start ()
	{
		var c = GameControl.Control;

		var stuck = true;

		if (c.Inventory.moneyAmount > 100)
		{
			stuck = false;
		}
		else if (c.Inventory.HasAHamster() && c.Inventory.moneyAmount > 10)
		{
			stuck = false;
		} else
		{
			if (c.Inventory.hamsterStates.Any(h => h != null && h.HamsterName.Length > 0 && h.foodLevel > 0))
			{
				stuck = false;
			}
		}

		if (stuck)
		{
			GameControl.Control.Inventory.AddMoney(100);
			GameControl.Control.SaveInventory();

			StartCoroutine(ShowMoneyGot());
		}
	}

	private IEnumerator ShowMoneyGot()
	{
		var cg = GetComponent<CanvasGroup>();
		cg.alpha = 1;
		yield return new WaitForSeconds(3);

		while (cg.alpha > 0.01)
		{
			cg.alpha = Mathf.Lerp(cg.alpha, 0, 0.8f);
			yield return new WaitForFixedUpdate();
		}

		cg.alpha = 0;
	}

}
