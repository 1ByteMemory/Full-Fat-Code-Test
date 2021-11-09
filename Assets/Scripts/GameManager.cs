using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject resultsPage;
	public Text finalScore;
	public static bool IsPlaying { get; private set; } = true;

	public static Event StartEvent;

	private void Start()
	{
		//StartGame();
	}

	public void GameOver()
	{
		finalScore.text = Mathf.FloorToInt(GetComponent<ScoreTracker>().score).ToString();
		resultsPage.SetActive(true);
		IsPlaying = false;
	}

	public void StartGame()
	{
		resultsPage.SetActive(false);
		IsPlaying = true;

		OnStartGame();
	}

	static void OnStartGame()
	{
		StartEvent?.Invoke();
	}
}
