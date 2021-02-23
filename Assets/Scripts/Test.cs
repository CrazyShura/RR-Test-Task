using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Test : MonoBehaviour
{
	#region Fields
	[SerializeField]
	int minChange = -3;
	[SerializeField]
	int maxChange = -1;
	[SerializeField]
	Hand hand;
	bool readyToTest = false;

	int counter = 0;
	#endregion

	#region Properties

	#endregion

	#region Methods
	private void Start()
	{
		PublicStuff.AddListenerToImagesLoaded(FillTheHand);
		StartCoroutine(PublicStuff.FillImageLibrary());
	}

	void FillTheHand()
	{
		for (int i = 0; i < Random.Range(4,7); i++)
		{
			GameObject cardObj = Instantiate((GameObject)Resources.Load("Prefubs/Cards/EmptyCard"));
			Card card = cardObj.GetComponent<Card>();
			string temp = PublicStuff.Prephics[Random.Range(0, PublicStuff.Prephics.Count)] + " " + PublicStuff.Names[Random.Range(0, PublicStuff.Names.Count)];
			cardObj.name = temp;
			card.Initilise(temp, Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10), PublicStuff.GetTexture());
			hand.AddCard(card);
		}
		readyToTest = true;
	}

	public void DoTheTest()
	{
		if(readyToTest)
		{
			Card card = hand.GetCard(counter);
			if(card != null)
			{
				card.ChangeStats(Random.Range( minChange, maxChange));
				counter++;
			}
			else
			{
				if(hand.CardsInHand > 0)
				{
					counter = 0;
					card = hand.GetCard(counter);
					card.ChangeStats(Random.Range(minChange, maxChange));
					counter++;
				}
				else
				{
					Debug.LogWarning("No more cards in hand");
				}
			}
		}
	}
	#endregion
}
