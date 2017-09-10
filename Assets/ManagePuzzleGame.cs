using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePuzzleGame : MonoBehaviour {

    public Image piece;
    public Image placeHolder;
    float phWidth, phHeight;

	// Use this for initialization
	void Start () {
        createPlaceHolder();
        createPieces();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void createPlaceHolder() {
        locateObjects("rightSide", "PlaceHolder", placeHolder);
    }

    void createPieces() {
		locateObjects("leftSide", "Piece", piece);

		Sprite[] allSprites = Resources.LoadAll<Sprite>("lion");
		for (int i = 0; i < 25; i++)
		{
            Image ph = GameObject.Find("Piece" + (i + 1)).GetComponent<Image>();
            ph.GetComponent<Image>().sprite = allSprites[i];
		}
	}

    void locateObjects(string location, string name, Image template) {
		phWidth = 100;
		phHeight = 100;
		float nbRows, nbColumns;
		nbRows = 5;
		nbColumns = 5;

        for (int i = 0; i < 25; i++)
        {
			Vector3 centerPosition = new Vector3();
			centerPosition = GameObject.Find(location).transform.position;

			float row, column;
			row = i % 5;
			column = i / 5;

			Vector3 phPosition = new Vector3(centerPosition.x + phWidth * (row - nbRows / 2),
											 centerPosition.y - phHeight * (column - nbColumns / 2),
											 centerPosition.z);

			Image ph = (Image)(Instantiate(template, phPosition, Quaternion.identity));

			ph.tag = "" + (i + 1);
			ph.name = name + (i + 1);
            ph.transform.SetParent(GameObject.Find("Canvas").transform);
		}
	}
}
