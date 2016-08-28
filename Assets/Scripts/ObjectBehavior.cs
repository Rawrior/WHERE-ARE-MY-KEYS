using UnityEngine;
using System.Collections;

public class ObjectBehavior : MonoBehaviour
{
	public bool IsPickedUp;
	public Vector3 TargetPosition;
	public float DistanceFromCamera;

	private Vector3 targetDirection;

	// Use this for initialization
	void Start ()
	{
		IsPickedUp = false;
		TargetPosition = Vector3.zero;
		targetDirection = Vector3.zero;
		DistanceFromCamera = 2f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (IsPickedUp)
		{
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
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().angularDrag = 0.5f;
		targetDirection = trgtpos - transform.position;
		Debug.Log(targetDirection);

		if (Mathf.Sqrt(targetDirection.sqrMagnitude) > 0.01f)
		{
			GetComponent<Rigidbody>().velocity = targetDirection;
		}

		//GetComponent<Rigidbody>().AddForce(targetDirection,ForceMode.Force);
	}
}
