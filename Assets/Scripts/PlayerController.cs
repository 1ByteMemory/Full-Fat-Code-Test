using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : InputManager
{
	public int lanePos;

	public float touchMoveDeadzone = 0.5f;
	

	bool ignoreInput;

    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
		
    }

    // Update is called once per frame
    protected override void Update()
    {
		base.Update();
    }


	public void MovePlayer(float delatX)
	{
		Debug.Log("Moving");
		if (!ignoreInput)
		{
			ignoreInput = true;

			if (delatX > touchMoveDeadzone)
			{
				// Move Right
				Debug.Log("Moving player Right");

				transform.Translate(Vector3.right, Space.Self);
			}
			else if (delatX < -touchMoveDeadzone)
			{
				// Move Left
				Debug.Log("Moving player Left");

				transform.Translate(Vector3.left, Space.Self);
			}
		}
	}


	protected override void OnTouchBegin()
	{
		ignoreInput = false;
	}

	protected override void OnTouchMove(Vector2 delta)
	{
		// Don't move player if swiping up or down
		if (delta.y <= 1 && delta.y >= -1)
			MovePlayer(delta.x);

	}

	protected override void OnTouchRelease()
	{
		ignoreInput = false;
	}
}
