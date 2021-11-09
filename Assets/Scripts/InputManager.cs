using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Event();

public class InputManager : MonoBehaviour
{

	private TouchPhase tp;
	
	
	protected virtual void Start()
	{
		GameManager.StartEvent += GameStart;
		GameStart();
	}
	protected virtual void GameStart()
	{
	}

	float prevH; // For keybored inputs

	// Update is called once per frame
	protected virtual void Update()
	{
		if (!GameManager.IsPlaying) return;
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

#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0))
			OnTouchBegin();

		if (Input.GetMouseButton(0) && Input.GetAxis("Mouse X") != 0)
		{
			OnTouchMove(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
		}

		if (Input.GetMouseButtonUp(0))
			OnTouchRelease();

		// Keybored Inputs
		float h = Input.GetAxisRaw("Horizontal");
		if (h != 0 && prevH == 0)
			OnTouchBegin();
		if (h != 0)
			OnTouchMove(new Vector2(h, 0));
		if (h == 0 && prevH != 0)
			OnTouchRelease();
		prevH = h;
#endif

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