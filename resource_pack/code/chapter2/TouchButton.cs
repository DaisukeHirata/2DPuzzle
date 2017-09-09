using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class TouchButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void touchButton()
	{

		//print ("You have pressed " + EventSystem.current.currentSelectedGameObject.name);
		int colorNumber = (int) (int.Parse(EventSystem.current.currentSelectedGameObject.tag));
		GameObject.Find ("gameManager").GetComponent<ManageAudioGame> ().submitColor (colorNumber);
	}
}
