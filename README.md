# UnityBridgeWithiOS

标签（空格分隔）： Steps

---

# Create an iOS plugin for Unity

### - Goal:
    Create an iOS plugin for Unity, so that both Unity and Xcode can communicate with each other.
    
### - Steps:
- Create extern code files in Xcode, for example: "UnityBridge.h", "UnityBridge.m"

- Write the extern function(s) in these two files, either in the implementation block or not. And **NO NEED** for add “ extern ‘C’ ” as the Unity official document says. Weird error will comes out if you add “extern ‘C’”

**Here’s the main part of “UnityBridge.m”:**
```
	void TalkingTest(char *msg) 
    {
        NSLog(@"UnityCall!");
        
        NSString *msgStr = [NSString stringWithUTF8String:msg];
        NSLog(@"Unity says: %@", msgStr);
        
        char *sendMsg = "Hello, Unity~";
        UnitySendMessage("Main Camera", "GetMsgFromiOS", sendMsg);    
    }
```

Pay attention to this **"UnitySendMessage"** function:

	UnitySendMessage(“GameObject”, “Method”, “Message to send”);

    parameters list: 
    	1. the name of the target GameObject;
    	2. the script method to call on that object;
    	3. the message string to pass to the called method.
    
    Note: 
    	1. Only script methods that correspond to the following signature can be called from native code: void MethodName(string msg)
    	2. Calls to UnitySendMessage are asynchronous and have a delay of one frame.
	
- In Unity side, create “Plugin” folder under “Assets” folder, then create “iOS” folder under “Plugin”

- Wrap all native code methods with an additional C# file, call it “NativeCodeWrapper.cs”, place it into “Plugin” folder. 
All the extern functions written in .m file will be referenced inside and called through this file.
    
**Main part of “NativeCodeWrapper.cs”:**
```
	//	Remember to add this namespace
	using System.Runtime.InteropServices;

	public class NativeCodeWrapper : MonoBehaviour {

	//	Define the extern method
	[DllImport ("__Internal")]
	private static extern void TalkingTest (string str_UnityToiOS);

	//	This function calls the extern “TalkingTest(string str)”,  since it’s public and static, it can be called from anywhere in your C# script
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
```

- Create a new script named “Bridge”, to call the “StartTalkinTest()” method, and receive messages sent from Xcode:
    
**Main part of “Bridge.cs”:**
```
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
```

- Build and Run, you will see both Xcode and Unity have get the expected message.
