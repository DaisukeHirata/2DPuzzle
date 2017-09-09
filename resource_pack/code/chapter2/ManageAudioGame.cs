using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ManageAudioGame : MonoBehaviour {
	int colorSubmitted;
	private const int STATE_PLAY_SEQUENCE = 1;
	private const int STATE_WAIT_FOR_USER_INPUT = 2;
	private const int STATE_PROCESS_USER_INPUT = 3;

	private int currentState;


	int[] sequenceOfColor, sequenceOfColorsSubmitted;
	int indexOfColor = 0;


	bool newColorHasBeengenerated, allBoxesDisplayed, startTimer, waitingTimerActivated, waitingTimeElapsed;
	float timer, waitingTime, timeDelayBetweenDisplays;
	int animationIndex, nbColorsSubmitted;

	// Use this for initialization
	void Start () {
		//HideBoxes ();
		//displayBox (1);

		startTimer = false;
		waitingTimerActivated = false;
		waitingTimeElapsed = false;
		waitingTime = 0;

		newColorHasBeengenerated = false;
		sequenceOfColorsSubmitted = new int[100];
		sequenceOfColor = new int[100];
		startTimer = false;
		waitingTimerActivated = false;
		allBoxesDisplayed = false;
		timer = 0;
		animationIndex = 0;
		timeDelayBetweenDisplays = 2;
		nbColorsSubmitted = 0;
	

		currentState = STATE_PLAY_SEQUENCE;
		sequenceOfColor = new int[100];

		//for testing
		//sequenceOfColor = new int[] {1,2,3,4,2};
		//indexOfColor = 4;

		updateUI ();

	}
	
	// Update is called once per frame
	void Update () {

		switch (currentState) 
		{
		case STATE_PLAY_SEQUENCE:
			if (!newColorHasBeengenerated) {
				initTimer ();
				generateNewColor ();
				newColorHasBeengenerated = true;
				allBoxesDisplayed = false;
				HideBoxes ();
			}
			timer += Time.deltaTime;
			if (startTimer && timer >= timeDelayBetweenDisplays && !waitingTimerActivated) {
				timer = 0;
				HideBoxes ();
				displayBox (sequenceOfColor [animationIndex]);
				animationIndex++;
				if (animationIndex >= indexOfColor) {

					startTimer = false;
					animationIndex = 0;
					waitingTimerActivated = true;
				}

			}
			if (waitingTimerActivated) {

				waitingTime += Time.deltaTime;
				if (waitingTime >= timeDelayBetweenDisplays) {

					waitingTime = 0;
					waitingTimerActivated = false;
					waitingTimeElapsed = true;

				}

			}
			if (waitingTimeElapsed) 
			{
				currentState = STATE_WAIT_FOR_USER_INPUT;
				nbColorsSubmitted = 0;
			}




			
			break;
		case STATE_WAIT_FOR_USER_INPUT:
			if (!allBoxesDisplayed) {
				setBoxColors ();
				allBoxesDisplayed = true;


			}
			break;
		case STATE_PROCESS_USER_INPUT:
			bool okResult = assessUserMove ();
			if (okResult) 
			{
				animationIndex = 0;
				newColorHasBeengenerated = false;
				timer = 0;
				waitingTimerActivated = false;
				waitingTimeElapsed = false;
				currentState = STATE_PLAY_SEQUENCE;


			}
			else{loadLoseLevel();}
			break;

		default:
			break;



		}
	
	}

	public void submitColor (int newColor)
	{
		playNote (newColor);
		print ("You have pressed color "+newColor);
		if (currentState == STATE_WAIT_FOR_USER_INPUT) 
		{

			colorSubmitted = newColor;
			sequenceOfColorsSubmitted [nbColorsSubmitted] = newColor;
			nbColorsSubmitted++;
			bool rightMove = assessUserCurrentMove (newColor);
			if (!rightMove)
				loadLoseLevel ();

			if (nbColorsSubmitted == indexOfColor) 
			{
				currentState = STATE_PROCESS_USER_INPUT;
			}

		}


	}

	void generateNewColor()
	{
		int r = Random.Range (1, 4);
		sequenceOfColor [indexOfColor] = r;
		indexOfColor++;

	}

	void HideBoxes()
	{
		for (int i = 1; i <= 4; i++) 
		{
			GameObject.FindGameObjectWithTag ("" + i).GetComponent<Image> ().enabled = false;
		}

	}

	public void displayBox(int index)
	{
		playNote (index);
		GameObject.FindWithTag ("" + index).GetComponent<Image> ().enabled = true;
	}

	void initTimer()
	{

		startTimer = true;
		timer = 0;
		animationIndex = 0;
	}

	void setBoxColors()
	{
		for (int i = 1; i <= 4; i++) {
			GameObject.FindWithTag ("" + i).GetComponent<Image> ().enabled = true;
		}
	}

	public bool assessUserMove()
	{

		bool allPerfect = true;
		for (int i = 0; i < indexOfColor; i++) 
		{
			int a = sequenceOfColor [i];
			int b = sequenceOfColorsSubmitted [i];
			if (a != b)
				allPerfect = false;
		}
		if (allPerfect) {print ("Well Done!"); updateUI ();return true;}
		else {print ("Not Right"); return false;}

		



	}

	public void loadLoseLevel()
	{
		PlayerPrefs.SetInt ("score", indexOfColor - 1);
		SceneManager.LoadScene ("chapter2_lose");

	}

	public void updateUI()
	{

		GameObject.Find ("nbMemorized").GetComponent<Text> ().text = "" + indexOfColor;
	}

	public void playNote (int index)
	{

		float note = (float)index;
		GetComponent<AudioSource> ().pitch = note;
		GetComponent<AudioSource> ().Play ();
	}

	public bool assessUserCurrentMove (int colorSubmitted)
	{

		if (sequenceOfColorsSubmitted [nbColorsSubmitted - 1] == sequenceOfColor [nbColorsSubmitted - 1])
			return true;
		else
			return false;
	}
}
