using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TouchEvent();

public class InputManager : MonoBehaviour
{
	public event TouchEvent TouchBegin;


	TouchPhase tp;

    // Start is called before the first frame update
    void Start()
    {
	}

    // Update is called once per frame
    void Update()
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
					break;
				case TouchPhase.Stationary:
					break;
				case TouchPhase.Ended:
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
		TouchBegin?.Invoke();
	}


	private void OnGUI()
	{
		GUI.TextArea(new Rect(10, 10, 100, 100), string.Format("Touch Phase: {0}", tp));	
	}
}
