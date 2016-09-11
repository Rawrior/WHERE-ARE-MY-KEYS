using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
	public LayerMask Mask;
	public GameObject LastHit;

	private bool isHoldingObject;

	// Use this for initialization
	void Start ()
	{
		Mask = ~Mask;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Debug.DrawRay(transform.position, transform.forward*3, Color.red);

		if (!isHoldingObject)
			RaycastMethod(Mask);

	}

	bool RaycastMethod(LayerMask layermask)
	{
		//Declare variable for the out-value of z
		RaycastHit RayHit;

		Physics.Raycast(transform.position, transform.forward*3, out RayHit, 3f, layermask);

		if (RayHit.collider != null)
		{
			if (RayHit.collider.gameObject != LastHit && LastHit != null)
			{
				LastHit.GetComponent<MeshRenderer>().material = LastHit.GetComponent<ObjectBehavior>().RestMaterial;
				//Changing to rest material	
				Debug.Log("Changing material to rest material");
				LastHit = RayHit.collider.gameObject;
			}

			if (RayHit.transform.tag == "Pickupable")
			{
				//Changing material to Hover material
				Debug.Log("Changing material to Hover material");
				LastHit = RayHit.collider.gameObject;
				RayHit.collider.GetComponent<MeshRenderer>().material = RayHit.collider.GetComponent<ObjectBehavior>().HoverMaterial;
			}

			Debug.Log("Returning true");
			return true;
		}

		if (RayHit.collider == null/* && LastHit != null*/)
		{
			//Changing material to Rest material
			Debug.Log("Changing material to rest material");
			LastHit.GetComponent<MeshRenderer>().material = LastHit.GetComponent<ObjectBehavior>().RestMaterial;
			LastHit = null;
			Debug.Log("Returning false");
			return false;
		}

		//if (RayHit.collider == null)
		//{
			
		//}
		return false;
	}
}
