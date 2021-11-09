using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform target;
	public float cameraShakeAmount;
	public float cameraShakeDuration;
	public float cameraShakeIntervals;

	bool isShaking;
	float shakeTimer;
	float intervalTimer;

	Vector3 startPos;
	private void Start()
	{
		startPos = transform.position;
	}

	// Update is called once per frame
	void Update()
    {
		Vector3 pos = transform.position;

		if (isShaking && Time.time < shakeTimer)
		{
			// Shake the camera
			if (Time.time < intervalTimer)
			{
				
				intervalTimer = Time.time + cameraShakeIntervals;

				float x = Random.Range(0, cameraShakeAmount);
				float y = Random.Range(0, cameraShakeAmount);
				float z = Random.Range(0, cameraShakeAmount);

				transform.position = new Vector3(x + startPos.x, y + startPos.y, z + target.position.z);
			}
		}
		else
		{
			isShaking = false;
			// update position to follow target, x and y position should stay the same
			transform.position = new Vector3(startPos.x, startPos.y, target.position.z);

		}
	}

	public void CameraShake()
	{
		if (!isShaking)
		{
			shakeTimer = Time.time + cameraShakeDuration;
			intervalTimer = Time.time + cameraShakeIntervals;
		}
		isShaking = true;
	}
}
