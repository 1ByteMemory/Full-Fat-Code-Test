using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Event();

public class InputManager : MonoBehaviour
{

	private TouchPhase tp;

	// Start is called before the first frame update
	protected virtual void Start()
	{
	}

	// Update is called once per frame
	protected virtual void Update()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			tp = touch.phase;

			switch (touch.phase)
			{
				case TouchPhase.Began:
					OnTouchBegin();
					break;
				case TouchPhase.Moved:
					OnTouchMove(touch.deltaPosition);
					break;
				case TouchPhase.Stationary:
					break;
				case TouchPhase.Ended:
					OnTouchRelease();
					break;
				case TouchPhase.Canceled:
					break;
				default:
					break;
			}

		}

	}

	protected virtual void OnTouchBegin()
	{

	}

	protected virtual void OnTouchMove(Vector2 delta)
	{

	}

	protected virtual void OnTouchRelease()
	{

	}

}