using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using CameraInfo = RosMessageTypes.MyObjectInfo.CameraInfoMsg;
using ObjectInfo = RosMessageTypes.MyObjectInfo.ObjectInfoMsg;
using ObjectInfoArray = RosMessageTypes.MyObjectInfo.ObjectInfoArrayMsg;
using System;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using UnityEngine.Pool;

public class DunamicModelPlacement : MonoBehaviour
{
    [SerializeField] private Camera virtualCamera;

    private GameObject[] modelPrefabs;

    [SerializeField] private bool debug_flag = true;

    private readonly string[] MODEL_NAMES = {
        "person", "bicycle", "car", "motorcycle", "airplane", "bus", "train", "truck", "boat",
        "traffic light", "fire hydrant", "stop sign", "parking meter", "bench", "bird", "cat", "dog", "horse", "sheep",
        "cow", "elephant", "bear", "zebra", "giraffe", "backpack", "umbrella", "handbag", "tie", "suitcase",
        "frisbee", "skis", "snowboard", "sports ball", "kite", "baseball bat", "baseball glove", "skateboard", "surfboard",
        "tennis racket", "bottle", "wine glass", "cup", "fork", "knife", "spoon", "bowl", "banana", "apple", "sandwich",
        "orange", "broccoli", "carrot", "hot dog", "pizza", "donut", "cake", "chair", "couch", "potted plant", "bed",
        "dining table", "toilet", "tv", "laptop", "mouse", "remote", "keyboard", "cell phone", "microwave", "oven",
        "toaster", "sink", "refrigerator", "book", "clock", "vase", "scissors", "teddy bear", "hair drier", "toothbrush"
    };

    // AddressはCellのheaderと連結リストを処理するためのメソッドがある
    private Address[] address;

    [SerializeField] private const string DYNAMIC_MODEL_TAG = "Dynamic Model";

    private void Start()
    {
        modelPrefabs = new GameObject[MODEL_NAMES.Length];
        for (int i = 0; i < MODEL_NAMES.Length; i++)
        {
            modelPrefabs[i] = (GameObject)Resources.Load<GameObject>(MODEL_NAMES[i]);
            //if (modelPrefabs[i] == null) modelPrefabs[i] = (GameObject)Resources.Load<GameObject>("default");
        }

        ROSConnection.GetOrCreateInstance().Subscribe<ObjectInfoArray>("/object_info", ModifyScene);

        address = new Address[MODEL_NAMES.Length];
        for (int i = 0; i < MODEL_NAMES.Length; i++)
        {
            address[i] = new Address();
        }
    }

    private void ModifyScene(ObjectInfoArray myMessage)
    {
        CameraInfo cameraInfo = myMessage.camera_info;
        ObjectInfo[] objectInfoArray = myMessage.object_info_array;

        UpdateCameraTransform(cameraInfo);
        //DeleteModel(); // 視野角内の物体を削除するものだけど，これをした場合見る角度によって物体認識ができなかった場合に削除してしまうからダメ，今後の課題とする
        PlaceModel(myMessage);


        // Debug.Log(objectInfoArray.Length)
        if (objectInfoArray.Length == 0)
        {
            debug_flag = false;
        }
        else
        {
            if (!debug_flag)
            {
                debug_flag = true;
                Debug.Log("現在時刻（ミリ秒）: " + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
            }
        }
    }

    // カメラの視野を変更
    private void UpdateCameraTransform(CameraInfo cameraInfo)
    {
        float cameraPosX = (float)cameraInfo.pos_x;
        float cameraPosY = (float)cameraInfo.pos_y;
        float cameraPosZ = (float)cameraInfo.pos_z;

        // Ubuntuで個人的に作った座標軸を，Unityの座標軸に変換したうえで，ラジアン角を度数に変換
        float yaw = 90 - (float)cameraInfo.yaw * Mathf.Rad2Deg;
        float pitch = -1 * (float)cameraInfo.pitch * Mathf.Rad2Deg;
        float roll = (float)cameraInfo.roll * Mathf.Rad2Deg;

        float valuableDepth = (float)cameraInfo.valuable_depth;

        // ラジアン角を度数に変換
        float horizontalAngle = (float)cameraInfo.horizontal_angle * Mathf.Rad2Deg;
        float verticalAngle = (float)cameraInfo.vertical_angle * Mathf.Rad2Deg;

        // カメラの位置と姿勢を更新
        virtualCamera.transform.position = new Vector3(cameraPosX, cameraPosY, cameraPosZ);
        virtualCamera.transform.rotation = Quaternion.Euler(pitch, yaw, roll);

        // カメラの最大深度を設定
        virtualCamera.farClipPlane = valuableDepth;

        // カメラの水平および垂直視野を設定
        virtualCamera.fieldOfView = verticalAngle;
        virtualCamera.aspect = horizontalAngle / verticalAngle;
    }

    // カメラの視野範囲内のモデルを削除
    private void DeleteModel()
    {
        // カメラの視錐台の平面を取得
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(virtualCamera);

        // カメラの視界内にあるオブジェクトを見つける
        Collider[] colliders = Physics.OverlapSphere(virtualCamera.transform.position, 100.0f);

        // 各オブジェクトがカメラの視界内にあるかどうか確認する
        foreach (Collider collider in colliders)
        {
            if (IsInFrustum(collider, frustumPlanes) && collider.CompareTag(DYNAMIC_MODEL_TAG))
            {
                // Destroy(collider.gameObject);
                collider.gameObject.SetActive(false);
            }
        }

        bool IsInFrustum(Collider collider, Plane[] frustumPlanes)
        {
            // オブジェクトのバウンズが視錐台の平面と交差しているか確認する
            Bounds bounds = collider.bounds;
            return GeometryUtility.TestPlanesAABB(frustumPlanes, bounds);
        }
    }

    // 検出したモデルを配置
    private void PlaceModel(ObjectInfoArray myMessage)
    {
        CameraInfo cameraInfo = myMessage.camera_info;
        ObjectInfo[] objectInfoArray = myMessage.object_info_array;

        foreach (ObjectInfo objectInfo in objectInfoArray)
        {
            // string objectName = objectInfo.object_name; // 名前の文字列は使ってないけど，わかりやすくデバッグ用にある
            int objectId = objectInfo.object_id;
            float objectPosX = (float)objectInfo.object_pos_x;
            float objectPosY = (float)objectInfo.object_pos_y;
            float objectPosZ = (float)objectInfo.object_pos_z;
            float objectWidth = (float)objectInfo.object_width;
            float objectHeight = (float)objectInfo.object_height;

            if (modelPrefabs[objectId] != null)
            {
                GameObject modelInstance = Instantiate(modelPrefabs[objectId]);
                modelInstance.transform.position = new Vector3(objectPosX, objectPosY, objectPosZ);
                modelInstance.transform.localScale = new Vector3(objectWidth, objectHeight, 0.1f); // ここモニタ用に変える

                // カメラの向きと逆を向くようにする
                // Ubuntuで個人的に作った座標軸を，Unityの座標軸に変換したうえで，ラジアン角を度数に変換
                float yaw = -90 - (float)cameraInfo.yaw * Mathf.Rad2Deg;
                float pitch = -1 * (float)cameraInfo.pitch * Mathf.Rad2Deg;
                float roll = (float)cameraInfo.roll * Mathf.Rad2Deg;
                modelInstance.transform.rotation = Quaternion.Euler(pitch, yaw, roll);

                modelInstance.tag = DYNAMIC_MODEL_TAG;

                address[objectId].InsertObjectData(modelInstance);
            }
        }
    }
}