using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
	public LayerMask Mask;
	public GameObject LastHit;

	// Use this for initialization
	void Start ()
	{
		Mask = ~Mask;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Debug.DrawRay(transform.position, transform.forward*3, Color.red);

		RaycastMethod(Mask);

	}

	void RaycastMethod(LayerMask layermask)
	{
		RaycastHit RayHit;
		if (Physics.Raycast(transform.position, transform.forward * 3, out RayHit, 3f, layermask))
		{
			//Physics.Raycast(transform.position, Vector3.forward*3, out RayHit, 100f, layermask);

			//TODO: Fix this shit. It's returning a nullreferenceexception from the editor. Something about the RayHit not being assigned? Research it and find out.
			Debug.Log(RayHit.collider.name);

			if (RayHit.transform.tag == "Pickupable")
			{
				LastHit = RayHit.collider.gameObject;
				RayHit.collider.GetComponent<MeshRenderer>().material = RayHit.collider.GetComponent<ObjectBehavior>().HoverMaterial;
			}
		}
	}
}
