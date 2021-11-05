using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : InputManager
{
	public int numberOfLanes = 5;
	public int lanePos;
	public int startingLanePos = 3;
	public float moveAmount = 2;

	public float speed = 0;
	public float speedCap = 100;
	public float speedIncreaseOverTime = 1;

	bool ignoreInput;

    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
		lanePos = startingLanePos;
		
    }

    // Update is called once per frame
    protected override void Update()
    {
		base.Update();

		if (speed < speedCap)
			speed += Time.deltaTime * speedIncreaseOverTime;
    }

	private void FixedUpdate()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
	}

	public void MovePlayer(float delatX)
	{
		if (!ignoreInput)
		{
			ignoreInput = true;

			if (delatX > 0 && lanePos + 1 <= numberOfLanes)
			{
				// Move Right if anble to

				Debug.Log("Moving player Right");
				lanePos++;
				transform.Translate(Vector3.right * moveAmount, Space.Self);

			}
			else if (delatX < 0 && lanePos - 1 > 0)
			{
				// Move Left
				Debug.Log("Moving player Left");

				lanePos--;
				transform.Translate(Vector3.left * moveAmount, Space.Self);
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
		//if (delta.y <= 10 && delta.y >= -10)  // this was arbitery so I commented it out
		MovePlayer(delta.x);

	}

	protected override void OnTouchRelease()
	{
		ignoreInput = false;
	}
}
