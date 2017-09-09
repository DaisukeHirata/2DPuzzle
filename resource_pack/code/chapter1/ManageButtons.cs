using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ManageButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("score", 0);
		if (SceneManager.GetActiveScene().name == "chapter1_start") PlayerPrefs.SetInt("score",0);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void startWordGame()
	{

		SceneManager.LoadScene ("chapter1");
	}
}
