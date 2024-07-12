using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using MyMessage = RosMessageTypes.MyObjectInfo.ObjectInfoMsg;

// ROS2とのカスタムメッセージの通信ができてるかどうかの確認スクリプト
public class MyCustomMessageDemo : MonoBehaviour
{
    /*
    private string objectName;
    private int objectId;
    private double objectPosX;
    private double objectPosY;
    private double objectPosZ;
    private double objectWidth;
    private double objectHeight;
    */

    private void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<MyMessage>("/object_info", MyDemo);
    }

    private void MyDemo(MyMessage myMessage)
    {
        /*
        objectName = myMessage.object_name;
        objectId = myMessage.object_id;
        objectPosX = myMessage.object_pos_x;
        objectPosY = myMessage.object_pos_y;
        objectPosZ = myMessage.object_pos_z;
        objectWidth = myMessage.object_width;
        objectHeight = myMessage.object_height;

        Debug.Log("物体名： " + objectName);
        Debug.Log("物体ID： " + objectId);
        Debug.Log("pos_x： " + objectPosX);
        Debug.Log("pos_y： " + objectPosY);
        Debug.Log("pos_z： " + objectPosZ);
        Debug.Log("物体幅： " + objectWidth);
        Debug.Log("物体高： " + objectHeight);
        */

        Debug.Log(myMessage);
    }
}