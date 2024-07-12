using RosMessageTypes.Sensor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UTF-8 with BOMのスクリプト
public class DemoDeletion : MonoBehaviour
{
    [SerializeField] private Camera virtualCamera;


    [SerializeField] private float cameraPosX;
    [SerializeField] private float cameraPosY;
    [SerializeField] private float cameraPosZ;

    [SerializeField] private float yaw;
    [SerializeField] private float pitch;
    [SerializeField] private float roll;

    [SerializeField] private float valuableDepth;
    [SerializeField] private float horizontalAngle;
    [SerializeField] private float verticalAngle;

    [SerializeField] private string targetTag;

    private void Start()
    {

    }

    private void Update()
    {
        // カメラの位置と姿勢を更新
        //UpdateCameraTransform();
        // モデルの削除
        DeleteModel();
    }

    private void UpdateCameraTransform()
    {
        Vector3 cameraPosition = new Vector3(cameraPosX, cameraPosY, cameraPosZ);
        Quaternion cameraRotation = Quaternion.Euler(-pitch, -yaw, roll);

        // カメラの位置と姿勢を更新
        virtualCamera.transform.position = cameraPosition;
        virtualCamera.transform.rotation = cameraRotation;

        // カメラの最大深度を設定
        //virtualCamera.farClipPlane = valuableDepth;

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
            if (IsInFrustum(collider, frustumPlanes) && collider.CompareTag(targetTag))
            {
                Destroy(collider.gameObject);
            }
        }

        bool IsInFrustum(Collider collider, Plane[] frustumPlanes)
        {
            // オブジェクトのバウンズが視錐台の平面と交差しているか確認する
            Bounds bounds = collider.bounds;
            return GeometryUtility.TestPlanesAABB(frustumPlanes, bounds);

        }
    }
}