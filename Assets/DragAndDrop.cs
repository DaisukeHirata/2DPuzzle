﻿using System.Collections;
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
}
