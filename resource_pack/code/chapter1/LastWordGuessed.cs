using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LastWordGuessed : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Text> ().text = PlayerPrefs.GetString ("lastWordGuessed");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
