using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    Vector3 originalPosition;

    // Use this for initialization
    void Start () {
        originalPosition = transform.position;
    }
	
    // Update is called once per frame
    void Update () {

    }

    public void Drag() {
        print("Dragging" + gameObject.name);
        gameObject.transform.position = Input.mousePosition;
    }

    public void Drop() {
        checkMatch();
    }

    public void checkMatch() {
		GameObject ph1 = GameObject.Find("PlaceHolder1");
		GameObject img = GameObject.Find("Image");

		float distance = Vector3.Distance(ph1.transform.position, img.transform.position);

        if (distance <= 50) {
            snap(img, ph1);
        } else {
            moveBack();
        }
	}

    public void moveBack() {
        transform.position = originalPosition;
    }

    public void snap(GameObject img, GameObject ph) {
		img.transform.position = ph.transform.position;
    }
}
