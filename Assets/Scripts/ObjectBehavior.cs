using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
	public bool IsPickedUp;
	public Vector3 TargetPosition;
	public float DistanceFromCamera;
	public Material RestMaterial;
	public Material HoverMaterial;
	public Material PickedUpMaterial;

	private Vector3 targetDirection;

	// Use this for initialization
	void Start ()
	{
		IsPickedUp = false;
		TargetPosition = Vector3.zero;
		targetDirection = Vector3.zero;
		//DistanceFromCamera = 2f;
	}
	
	// Update is called once per frame
	//void Update ()
	//{

	//}

	void FixedUpdate()
	{
		if (IsPickedUp)
		{
			//Debug.Log(TargetPosition);
			LerpFunction(TargetPosition);
		}
		else
		{
			GetComponent<Rigidbody>().useGravity = true;
			//GetComponent<Rigidbody>().angularDrag = 0.05f;
		}
	}

	void LerpFunction(Vector3 trgtpos)
	{
		//Set-up rules for when object is being handled
		//---------------------------------------------

		//Turn gravity off so the object doesn't sag in our grip.
		GetComponent<Rigidbody>().useGravity = false;
		//Increase drag to slow rotation down faster.
		GetComponent<Rigidbody>().angularDrag = 0.75f;

		//Set up the target direction. Effectively this is just the difference in target position and current position.
		targetDirection = (trgtpos - transform.position);

		//Move object to point with an overshoot effect
		if (Mathf.Sqrt(targetDirection.sqrMagnitude) > 0.01f)
		{
			GetComponent<Rigidbody>().AddForce(targetDirection * 0.9f, ForceMode.Impulse); /*velocity = targetDirection;*/
			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 0.9f;
		}
		else
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}

		//Stop rotation under certain angular velocity
		if (GetComponent<Rigidbody>().angularVelocity.magnitude < 0.05f)
		{
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}

		//Debug.Log("Linear velocity: " + GetComponent<Rigidbody>().velocity.magnitude);
		//Debug.Log("Angular velocity: " + GetComponent<Rigidbody>().angularVelocity.magnitude);
	}
}
