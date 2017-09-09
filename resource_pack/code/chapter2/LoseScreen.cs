using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find ("scoreUI").GetComponent<Text> ().text = "Score:" + PlayerPrefs.GetInt ("score");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
