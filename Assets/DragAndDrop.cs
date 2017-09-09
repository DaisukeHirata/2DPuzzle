using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }
	
    // Update is called once per frame
    void Update () {

    }

    public void Drag() {
        GameObject.Find("Image").transform.position = Input.mousePosition;
        print("Dragging" + gameObject.name);
    }

    public void Drop() {
        GameObject ph1 = GameObject.Find("PlaceHolder1");
        GameObject img = GameObject.Find("Image");

        float distance = Vector3.Distance(ph1.transform.position, img.transform.position);

        if (distance < 50) {
            img.transform.position = ph1.transform.position;
        }
    }
}
