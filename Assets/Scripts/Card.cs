using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
	#region Fields
	[SerializeField]
	int health = 1;
	[SerializeField]
	int attack = 1;
	[SerializeField]
	int cost = 1;
	[SerializeField]
	string cardName = "I Have No Name";
	[SerializeField]
	Text cardNameText, healthCounter, attackCounter, costCounter;
	[SerializeField]
	RawImage image;

	CardDead deadEvent = new CardDead();
	bool initialized = false;
	#endregion

	#region Properties
	public bool Initialized
	{
		get { return initialized; }
	}
	#endregion

	#region Methods
	private void Awake()
	{
		cardNameText.text = cardName;
		healthCounter.text = health.ToString();
		attackCounter.text = attack.ToString();
		costCounter.text = cost.ToString();
	}

	public void Initilise(string Name, int Health, int Attack, int Cost, Texture Image)
	{
		cardNameText.text = Name;
		health = Health;
		healthCounter.text = Health.ToString();
		attack = Attack;
		attackCounter.text = Attack.ToString();
		cost = Cost;
		costCounter.text = Cost.ToString();
		image.texture = Image;
		initialized = true;
	}

	/// <summary>
	/// Changes one of the stats of a card by Amount.
	/// </summary>
	/// <param name="ChangeAmount"></param>
	public void ChangeStats(int ChangeAmount)
	{
		switch (Random.Range(0, 3))
		{
			case 0:
				{
					health += ChangeAmount;
					if (health <= 0)
					{
						deadEvent.Invoke(this);
						Debug.Log(this.name + " health reached 0 and is now dead");
						Destroy(this.gameObject);
					}
					else
					{
						healthCounter.text = health.ToString();
						Debug.Log(this.name + " health changed to " + health);
					}
					break;
				}
			case 1:
				{
					attack += ChangeAmount;
					if (attack < 0)
					{
						Debug.Log(this.name + " attack cant be negative and insted set to 0");
						attack = 0;
					}
					else
					{
						Debug.Log(this.name + " attack changed to " + attack);
					}
					attackCounter.text = attack.ToString();
					break;
				}
			case 2:
				{
					cost += ChangeAmount;
					if (cost < 0)
					{
						Debug.Log(this.name + " cost cant be negative and insted set to 0");
						cost = 0;
					}
					else
					{
						Debug.Log(this.name + " cost changed to " + cost);
					}
					costCounter.text = cost.ToString();
					break;
				}
		}
	}

	public void AddListenersForCardDeath(UnityAction<Card> Action)
	{
		deadEvent.AddListener(Action);
	}

	public void RemoveListenersForCardDeath()
	{
		deadEvent.RemoveAllListeners();
	}
	#endregion
}
