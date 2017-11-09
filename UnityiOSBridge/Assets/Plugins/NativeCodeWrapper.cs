using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class NativeCodeWrapper : MonoBehaviour {
	
	[DllImport ("__Internal")]
	private static extern void TalkingTest (string str_UnityToiOS);

	public static bool StartTalking(string talkingMsg) {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			TalkingTest (talkingMsg);
			return true;
		} else {
			Debug.Log ("Editor call");
			return false;
		}
	}
}
