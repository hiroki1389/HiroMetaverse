//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.MyObjectInfo
{
    [Serializable]
    public class ObjectInfoArrayMsg : Message
    {
        public const string k_RosMessageName = "my_object_info_msgs/ObjectInfoArray";
        public override string RosMessageName => k_RosMessageName;

        // ObjectInfoArray.msg
        public CameraInfoMsg camera_info;
        public ObjectInfoMsg[] object_info_array;

        public ObjectInfoArrayMsg()
        {
            this.camera_info = new CameraInfoMsg();
            this.object_info_array = new ObjectInfoMsg[0];
        }

        public ObjectInfoArrayMsg(CameraInfoMsg camera_info, ObjectInfoMsg[] object_info_array)
        {
            this.camera_info = camera_info;
            this.object_info_array = object_info_array;
        }

        public static ObjectInfoArrayMsg Deserialize(MessageDeserializer deserializer) => new ObjectInfoArrayMsg(deserializer);

        private ObjectInfoArrayMsg(MessageDeserializer deserializer)
        {
            this.camera_info = CameraInfoMsg.Deserialize(deserializer);
            deserializer.Read(out this.object_info_array, ObjectInfoMsg.Deserialize, deserializer.ReadLength());
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.camera_info);
            serializer.WriteLength(this.object_info_array);
            serializer.Write(this.object_info_array);
        }

        public override string ToString()
        {
            return "ObjectInfoArrayMsg: " +
            "\ncamera_info: " + camera_info.ToString() +
            "\nobject_info_array: " + System.String.Join(", ", object_info_array.ToList());
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
