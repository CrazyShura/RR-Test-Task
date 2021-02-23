using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	#region Fields
	List<Card> content = new List<Card>();
	#endregion

	#region Properties

	#endregion

	#region Methods
	private void Awake()
	{
		this.transform.tag = "Board";
	}

	public void AddCard(Card CardToAdd)
	{
		content.Add(CardToAdd);
		RecalculateCardPositions();
	}

	void RecalculateCardPositions()
	{
		foreach (Card card in content)
		{
			card.transform.GetChild(0).GetComponent<Canvas>().sortingOrder = content.IndexOf(card);
			Vector3 targetPos = this.transform.position - new Vector3((-content.IndexOf(card) * 2f + ((((float)content.Count) - 1) / 2)), -3, 0);
			card.GetComponent<CardMovement>().SetTargetPosition(targetPos);
			card.GetComponent<CardMovement>().SetHome();
			card.GetComponent<CardMovement>().MoveEnabled = false;
		}
	}
	#endregion
}
