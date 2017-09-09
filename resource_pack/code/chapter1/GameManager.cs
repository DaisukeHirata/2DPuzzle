using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject letter;
	public GameObject cen;


	private string wordToGuess = "";
	private string [] wordsToGuess = new string[] {"car", "elephant", "autocar"};
	private int lenghtOfWordToGuess;
	char [] lettersToGuess;
	bool [] lettersGuessed;

	private int nbAttempts, maxNbAttemps;

	int score = 0;


	// Use this for initialization
	void Start () {
		cen = GameObject.Find ("centerOfScreen");
		initGame ();
		initLetters ();
		nbAttempts = 0;
		maxNbAttemps = 10;
		updateScore ();
	}
	
	// Update is called once per frame
	void Update () {
	
		//checkKeyboard ();
		checkKeyboard2 ();
	}

	void initLetters()
	{
		int nbLetters = lenghtOfWordToGuess;
		for (int i = 0; i < nbLetters; i++) 
		{
			Vector3 newPosition;
			//newPosition = new Vector3 (transform.position.x + (i * 100), transform.position.y, transform.position.z);
			//newPosition = new Vector3 (cen.transform.position.x + (i * 100), cen.transform.position.y, cen.transform.position.z);
			newPosition = new Vector3 (cen.transform.position.x + ((i-nbLetters/2.0f) * 100), cen.transform.position.y, cen.transform.position.z);
			GameObject l = (GameObject)Instantiate (letter, newPosition, Quaternion.identity);
			l.name = "letter" + (i + 1);
			//l.transform.parent = GameObject.Find ("Canvas").transform;
			l.transform.SetParent (GameObject.Find ("Canvas").transform);


		}


	}

	void initGame()
	{

		//wordToGuess = "Elephant";
		int randomNumber = Random.Range (0, wordsToGuess.Length -1);
		//wordToGuess = wordsToGuess [randomNumber];
		wordToGuess = pickAWordFromFile();

		lenghtOfWordToGuess = wordToGuess.Length;
		wordToGuess = wordToGuess.ToUpper ();
		lettersToGuess = new char [lenghtOfWordToGuess];
		lettersGuessed = new bool [lenghtOfWordToGuess];
		lettersToGuess = wordToGuess.ToCharArray();


	}

	void checkKeyboard()
	{

		if (Input.GetKeyDown(KeyCode.A))
		{
				for (int i = 0; i < lenghtOfWordToGuess; i ++)
				{

					if (!lettersGuessed[i])
					{

						if (lettersToGuess [i] == 'A')
						{
							lettersGuessed[i] = true;
							GameObject.Find("letter"+(i+1)).GetComponent<Text>().text = "A";


						}


					}

				}
		


		}




	}

	void checkKeyboard2()
	{

		if (Input.anyKeyDown) 
		{
			char letterPressed = Input.inputString.ToCharArray () [0];
			int letterPressedAsInt = System.Convert.ToInt32 (letterPressed);
			if (letterPressedAsInt >= 97 && letterPressedAsInt <= 122) 
			{
				nbAttempts++; updateNbAttempts();
				if (nbAttempts > maxNbAttemps) 
				{

					SceneManager.LoadScene ("chapter1_lose");
				}
				for (int i = 0; i < lenghtOfWordToGuess; i++) 
				{
					if (!lettersGuessed [i]) 
					{
						letterPressed = System.Char.ToUpper (letterPressed);
						if (lettersToGuess [i] == letterPressed) 
						{
							lettersGuessed [i] = true;
							GameObject.Find("letter"+(i+1)).GetComponent<Text>().text = letterPressed.ToString();
							score = PlayerPrefs.GetInt ("score");
							score++;
							PlayerPrefs.SetInt ("score", score);
							updateScore ();
							checkifWordWasFound ();
						}

					}


				}


			}


		}
	}

	void updateNbAttempts()
	{
		GameObject.Find ("nbAttempts").GetComponent<Text> ().text = nbAttempts + "/" + maxNbAttemps;

	}

	void updateScore()
	{

		GameObject.Find ("scoreUI").GetComponent<Text> ().text = "Score"+score;

	}

	void checkifWordWasFound()
	{

		bool condition = true;
		for (int i = 0; i < lenghtOfWordToGuess; i++) 
		{
			condition = condition && lettersGuessed [i];


		}
		if (condition) 
		{
			PlayerPrefs.SetString ("lastWordGuessed", wordToGuess);
			SceneManager.LoadScene ("chapter1_well_done");

		}

	}
	string pickAWordFromFile()
	{
		TextAsset t1 = (TextAsset)Resources.Load ("words", typeof(TextAsset));
		string s = t1.text;
		string[] words = s.Split ("\n" [0]);
		int randomWord = Random.Range (0, words.Length + 1);
		return (words [randomWord]);

	}
}
