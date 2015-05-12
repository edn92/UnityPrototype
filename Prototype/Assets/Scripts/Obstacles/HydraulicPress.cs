using UnityEngine;
using System.Collections;

public class HydraulicPress : MonoBehaviour {
	public int slideSpeed;

	SliderJoint2D slider;
	JointMotor2D motor;
	bool isDoorOpen = false;
	// Use this for initialization
	void Start () {
		slider = gameObject.GetComponent<SliderJoint2D> ();
		motor = slider.motor;
	}
	
	// Update is called once per frame
	void Update () {
		//check if the spacebar key is pressed
		if(Input.GetKeyDown(KeyCode.Space)){
			//check if the door is already open
			if(!isDoorOpen)
				//set the motor speed to +100 so that the door will slide open
				motor.motorSpeed = slideSpeed;
			else
				//set the motor speed to -100 so that the door will close
				motor.motorSpeed = -slideSpeed;
			//invert the isDoorOpen flag once the door is opened or closed
			isDoorOpen = !isDoorOpen;
		}
		//set the sliderJoint2D's motor speed
		slider.motor = motor;
	}
}
