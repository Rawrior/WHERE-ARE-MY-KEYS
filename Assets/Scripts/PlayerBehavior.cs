using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
	public LayerMask Mask;
	public GameObject LastHit;
	public GameObject LastObjectHit;

	private bool isHoldingObject;

	private RaycastHit rayHit;

	// Use this for initialization
	void Start ()
	{
		Mask = ~Mask;
		isHoldingObject = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.DrawRay(transform.position, transform.forward*3, Color.red);

		if (!isHoldingObject && RaycastMethod(Mask) && Input.GetKey(KeyCode.Mouse0))
		{
			//Debug.Log("Trying to pick up object.");
			isHoldingObject = true;
		}

		if (LastObjectHit != null && Input.GetKeyUp(KeyCode.Mouse0))
		{
			//Debug.Log("Trying to drop object");
			isHoldingObject = false;
			//Debug.Log("Setting the material");
			LastObjectHit.GetComponent<MeshRenderer>().material = LastObjectHit.GetComponent<ObjectBehavior>().RestMaterial;
			//Debug.Log("Setting the picked-up bool to false");
			LastObjectHit.GetComponent<ObjectBehavior>().IsPickedUp = false;
		}

		if (isHoldingObject)
		{
			//Debug.Log("Setting target position");
			LastObjectHit.GetComponent<ObjectBehavior>().TargetPosition =
				transform.position + transform.forward * LastObjectHit.GetComponent<ObjectBehavior>().DistanceFromCamera;
			//Debug.Log("Setting the material");
			LastObjectHit.GetComponent<MeshRenderer>().material = LastObjectHit.GetComponent<ObjectBehavior>().PickedUpMaterial;
			//Debug.Log("Setting the picked-up bool to true");
			LastObjectHit.GetComponent<ObjectBehavior>().IsPickedUp = true;
		}

		//if (RaycastMethod(Mask) && !isHoldingObject && Input.GetKey(KeyCode.Mouse0))
		//{
		//	isHoldingObject = true;
		//	LastHit.GetComponent<ObjectBehavior>().TargetPosition = 
		//		transform.position + transform.forward * rayHit.collider.GetComponent<ObjectBehavior>().DistanceFromCamera;
		//	LastHit.GetComponent<ObjectBehavior>().IsPickedUp = true;
		//}

		//if (isHoldingObject && Input.GetKeyUp(KeyCode.Mouse0))
		//{
		//	isHoldingObject = false;
		//	rayHit.collider.GetComponent<ObjectBehavior>().IsPickedUp = false;
		//}

	}

	bool RaycastMethod(LayerMask layermask)
	{
		//Declare variable for the out-value of z


		Physics.Raycast(transform.position, transform.forward*3, out rayHit, 3f, layermask);

		if (rayHit.collider != null)
		{
			if (rayHit.collider.gameObject != LastHit && LastHit != null)
			{
				LastHit.GetComponent<MeshRenderer>().material = LastHit.GetComponent<ObjectBehavior>().RestMaterial;
				//Changing to rest material	
				//Debug.Log("Changing material to rest material");
				LastHit = rayHit.collider.gameObject;
				LastObjectHit = rayHit.collider.gameObject;
			}

			if (rayHit.transform.tag == "Pickupable")
			{
				//Changing material to Hover material
				//Debug.Log("Changing material to Hover material");
				LastHit = rayHit.collider.gameObject;
				LastObjectHit = rayHit.collider.gameObject;
				rayHit.collider.GetComponent<MeshRenderer>().material = rayHit.collider.GetComponent<ObjectBehavior>().HoverMaterial;
			}

			//Debug.Log("Returning true");
			return true;
		}

		if (rayHit.collider == null && LastHit != null)
		{
			//Changing material to Rest material
			//Debug.Log("Changing material to rest material");
			LastHit.GetComponent<MeshRenderer>().material = LastHit.GetComponent<ObjectBehavior>().RestMaterial;
			LastHit = null;
			return false;
		}

		//if (rayHit.collider == null)
		//{

		//}
		return false;
	}
}
