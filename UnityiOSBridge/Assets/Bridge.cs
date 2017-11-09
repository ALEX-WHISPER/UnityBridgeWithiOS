using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bridge : MonoBehaviour {
	public Text t_Msg;
	public Text t_Status;

	//	call NativeCodeWrapper.StartTalking() function, to send msg to ios
	public void OnStartTalking() {
		string talkingMsg = "Hello, iOS~";
		if (NativeCodeWrapper.StartTalking (talkingMsg)) {
			t_Status.text = "StartInDevice";
		} else {
			t_Status.text = "StartInEditor";
		}
	}

	//	Receive msg from ios
	public void GetMsgFromiOS(string msg) {
		if (!string.IsNullOrEmpty(msg)) {
			t_Msg.text = msg;
			t_Status.text = "Get message from iOS";
		}
	}
}
