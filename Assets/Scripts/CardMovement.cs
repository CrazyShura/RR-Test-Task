using UnityEngine;

[RequireComponent(typeof(Card))]
public class CardMovement : MonoBehaviour
{
	#region Fields
	[SerializeField]
	float speed = 1f;
	[SerializeField]
	float rotSpeed = .1f;
	[SerializeField]
	bool moveEnabled = true;

	Vector3 targetLocation = Vector3.zero;
	Quaternion targetRotation = Quaternion.identity;

	Vector3 home = Vector3.zero;
	Quaternion homeRot = Quaternion.identity;

	#endregion

	#region Properties
	public bool MoveEnabled { get => moveEnabled; set => moveEnabled = value; }
	#endregion

	#region Methods
	private void Update()
	{
		if (this.transform.position != targetLocation)
		{
			this.transform.position = Vector3.Lerp(this.transform.position, targetLocation, Time.deltaTime * speed);
		}
		if (targetRotation != this.transform.rotation)
		{
			this.transform.rotation = Quaternion.Lerp(targetRotation, this.transform.rotation, Time.deltaTime * rotSpeed);
		}
	}

	public void SetTargetPosition(Vector3 NewTarget)
	{
		targetLocation = NewTarget;
	}

	public void SetTargetRotation(Quaternion NewRotation)
	{
		targetRotation = NewRotation;
	}

	public void SetHome()
	{
		home = targetLocation;
		homeRot = targetRotation;
	}

	private void OnMouseDown()
	{
		if (MoveEnabled)
		{
			targetRotation = Quaternion.identity;
		}
	}

	private void OnMouseDrag()
	{
		if (MoveEnabled)
		{
			Plane plane = new Plane(Vector3.up, 0);
			Vector3 anchor = Vector3.zero;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float dist;
			if (plane.Raycast(ray, out dist))
			{
				anchor = ray.GetPoint(dist);
			}
			targetLocation = anchor;
		}
	}
	private void OnMouseUp()
	{
		if (MoveEnabled)
		{
			RaycastHit hit;
			if (Physics.Raycast(this.transform.position, Vector3.down, out hit))
			{
				if (hit.collider.CompareTag("Board"))
				{
					PublicStuff.Hand.RemoveCard(this.gameObject.GetComponent<Card>());
					hit.collider.gameObject.GetComponent<Board>().AddCard(this.gameObject.GetComponent<Card>());
					return;
				}
			}
			targetLocation = home;
			targetRotation = homeRot;
		}
	}
	#endregion
}
