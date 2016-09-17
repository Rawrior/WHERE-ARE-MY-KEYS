using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject Player;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Player.transform.position = new Vector3(0,5,0);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Player.transform.position = new Vector3(14,23,-52.5f);
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			Player.transform.position = new Vector3(0, 1.5f, 19);
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(0);
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
