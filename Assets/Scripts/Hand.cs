using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	#region Fields
	List<CardMovement> content = new List<CardMovement>();
	#endregion

	#region Properties
	public int CardsInHand
	{
		get{ return content.Count; }
	}
	#endregion

	#region Methods
	private void Awake()
	{
		PublicStuff.SetHand(this);
	}

	public void AddCard(Card CardToAdd)
	{
		content.Add(CardToAdd.GetComponent<CardMovement>());
		CardToAdd.AddListenersForCardDeath(RemoveCard);
		RecalculateCardPositions();
	}

	public void RemoveCard(Card CardToRemove)
	{
		if(content.Contains(CardToRemove.GetComponent<CardMovement>()))
		{
			content.Remove(CardToRemove.GetComponent<CardMovement>());
		}
		else
		{
			Debug.LogError("Trying to remove card that is not in hand");
		}
		RecalculateCardPositions();
	}

	void RecalculateCardPositions()
	{
		foreach (CardMovement card in content)
		{
			card.transform.GetChild(0).GetComponent<Canvas>().sortingOrder = content.IndexOf(card);
			Vector3 targetPos = this.transform.position - new Vector3(-content.IndexOf(card) + ((((float)content.Count) - 1) / 2), 0, .3f * Mathf.Abs(((((float)content.Count) - 1) / 2) - content.IndexOf(card)));
			card.SetTargetPosition(targetPos);
			Quaternion targetRot = Quaternion.FromToRotation(targetPos, Vector3.back);
			card.SetTargetRotation(targetRot);
			card.SetHome();
		}
	}

	/// <summary>
	/// Returns a card from hand or returns null if index larger then amount of cards.
	/// </summary>
	/// <param name="CardIndex"></param>
	/// <returns></returns>
	public Card GetCard(int CardIndex)
	{
		if(content.Count > CardIndex)
		{
			return content[CardIndex].GetComponent<Card>();
		}
		else
		{
			return null;
		}
	}
	#endregion
}
