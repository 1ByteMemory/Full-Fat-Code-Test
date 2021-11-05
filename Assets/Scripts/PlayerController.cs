using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
		if (FindObjectOfType<InputManager>())
		{
			inputManager = FindObjectOfType<InputManager>();
			inputManager.TouchBegin += MovePlayer;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public static void MovePlayer()
	{
		Debug.Log("Moving player");
	}
}
