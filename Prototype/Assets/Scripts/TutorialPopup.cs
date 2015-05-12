using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialPopup : MonoBehaviour {
	public string popupMessage;
	private Text popup;
	private Image closeButton;
	private Image background;
	// Use this for initialization
	void Start () {
		background = GameObject.Find ("PopupBG").GetComponent<Image> ();
		popup = GameObject.Find ("PopupText").GetComponent<Text> ();
		closeButton = GameObject.Find ("CloseButton").GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			background.enabled = true;
			popup.enabled = true;
			popup.text = popupMessage;
			closeButton.enabled = true;

			Destroy (gameObject);
		}
	}
}
