using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour {
	private Image background;
	private Image closeButton;
	private Text popup;
	// Use this for initialization
	void Start () {
		background = GameObject.Find ("PopupBG").GetComponent<Image> ();
		closeButton = GameObject.Find ("CloseButton").GetComponent<Image>();
		popup = GameObject.Find ("PopupText").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClosePopup(){
		background.enabled = false;
		closeButton.enabled = false;
		popup.enabled = false;
	}
}
