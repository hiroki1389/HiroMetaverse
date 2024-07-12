//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.MyObjectInfo
{
    [Serializable]
    public class CameraInfoMsg : Message
    {
        public const string k_RosMessageName = "my_object_info_msgs/CameraInfo";
        public override string RosMessageName => k_RosMessageName;

        // CameraInfo.msg
        public double pos_x;
        public double pos_y;
        public double pos_z;
        public double yaw;
        public double pitch;
        public double roll;
        public double valuable_depth;
        public double horizontal_angle;
        public double vertical_angle;

        public CameraInfoMsg()
        {
            this.pos_x = 0.0;
            this.pos_y = 0.0;
            this.pos_z = 0.0;
            this.yaw = 0.0;
            this.pitch = 0.0;
            this.roll = 0.0;
            this.valuable_depth = 0.0;
            this.horizontal_angle = 0.0;
            this.vertical_angle = 0.0;
        }

        public CameraInfoMsg(double pos_x, double pos_y, double pos_z, double yaw, double pitch, double roll, double valuable_depth, double horizontal_angle, double vertical_angle)
        {
            this.pos_x = pos_x;
            this.pos_y = pos_y;
            this.pos_z = pos_z;
            this.yaw = yaw;
            this.pitch = pitch;
            this.roll = roll;
            this.valuable_depth = valuable_depth;
            this.horizontal_angle = horizontal_angle;
            this.vertical_angle = vertical_angle;
        }

        public static CameraInfoMsg Deserialize(MessageDeserializer deserializer) => new CameraInfoMsg(deserializer);

        private CameraInfoMsg(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.pos_x);
            deserializer.Read(out this.pos_y);
            deserializer.Read(out this.pos_z);
            deserializer.Read(out this.yaw);
            deserializer.Read(out this.pitch);
            deserializer.Read(out this.roll);
            deserializer.Read(out this.valuable_depth);
            deserializer.Read(out this.horizontal_angle);
            deserializer.Read(out this.vertical_angle);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.pos_x);
            serializer.Write(this.pos_y);
            serializer.Write(this.pos_z);
            serializer.Write(this.yaw);
            serializer.Write(this.pitch);
            serializer.Write(this.roll);
            serializer.Write(this.valuable_depth);
            serializer.Write(this.horizontal_angle);
            serializer.Write(this.vertical_angle);
        }

        public override string ToString()
        {
            return "CameraInfoMsg: " +
            "\npos_x: " + pos_x.ToString() +
            "\npos_y: " + pos_y.ToString() +
            "\npos_z: " + pos_z.ToString() +
            "\nyaw: " + yaw.ToString() +
            "\npitch: " + pitch.ToString() +
            "\nroll: " + roll.ToString() +
            "\nvaluable_depth: " + valuable_depth.ToString() +
            "\nhorizontal_angle: " + horizontal_angle.ToString() +
            "\nvertical_angle: " + vertical_angle.ToString();
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