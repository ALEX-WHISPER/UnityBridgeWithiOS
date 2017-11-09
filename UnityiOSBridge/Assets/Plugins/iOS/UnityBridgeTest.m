//
//  UnityBridgeTest.m
//  
//
//  Created by Yichen Zhong on 2017/11/9.
//

#import "UnityBridgeTest.h"

@interface UnityBridgeTest()

@property (weak, nonatomic) NSString *msgToUnity;
@property(weak, nonatomic) NSString *msgFromUnity;

@end


@implementation UnityBridgeTest
/*- (void)TalkingTest:(NSString*)msg {
 
 //  Get message from unity
 self.msgFromUnity = msg;
 NSLog(@"Get message from unity: %@", self.msgFromUnity);
 
 //  Send message to Unity
 self.msgToUnity = @"Hello, Unity!";
 const char *sendMsg = [self.msgToUnity UTF8String];
 UnitySendMessage("MainCamera", "GetMsgFromiOS", sendMsg);
 NSLog(@"Send message to unity: %@", self.msgToUnity);
 }*/
@end

void TalkingTest(char *msg) {
    NSLog(@"UnityCall!");
    
    NSString *msgStr = [NSString stringWithUTF8String:msg];
    NSLog(@"Unity says: %@", msgStr);
    
    char *sendMsg = "Hello, Unity~";
    UnitySendMessage("Main Camera", "GetMsgFromiOS", sendMsg);
}

