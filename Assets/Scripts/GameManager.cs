using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Text TimerText;
	public bool playing;
	private float Timer;
	public ParticleSystem winParticles;

	// Boolean to trigger win only once
	bool winTrig = true;

	void Update()
	{
		// Activate a timer on the text UI
		if (playing == true)
		{
			Timer += Time.deltaTime;
			int minutes = Mathf.FloorToInt(Timer / 60F);
			int seconds = Mathf.FloorToInt(Timer % 60F);
			int milliseconds = Mathf.FloorToInt((Timer * 100F) % 100F);
			TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
		}

		// If all bags are ready, check if all orders are correct, stop timer if they are correct 
		if (BurgerOrders.leftOrderCorrect && BurgerOrders.middleOrderCorrect && BurgerOrders.rightOrderCorrect && winTrig)
		{
			winTrig = false;
			playing = false;
			Debug.Log("Tout les burgers sont corrects, le jeu est fini !");
			winParticles.Play();
		}
	}
}
