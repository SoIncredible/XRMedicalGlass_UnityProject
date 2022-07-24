
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SmoothController : MonoBehaviour
{
    public Transform Target = null;
    //public Camera MainCamera = null;
    public GameObject MainCamera = null;
    public GameObject model;
    private float XAngle = 0f;
    private float YAngle = 0f;
    private float XSpeed = 250f;
    private float YSpeed = 120f;

    private float Distance = 5f;
    //private float DisSpeed = 0.005f;
    private float DisSpeed = 0.1f;
    private float MinDistance = 0f;
    private float MaxDistance = 50f;

    private Vector2 Vec2Pos1 = Vector2.zero;
    private Vector2 Vec2Pos2 = Vector2.zero;

    void Start()
    {
        /*
        model = GameObject.Find("DataPipeline").GetComponent<GetModel>().Model;
        GameObject a = Instantiate<GameObject>(model, new Vector3(0, 0, 0), Quaternion.identity);
        Target = a.GetComponent<Transform>();
        MainCamera = Camera.main;
        */
        MainCamera = GameObject.Find("XR Origin/Camera Offset/Main Camera");
    }

    // void OnGUI()
    // {
    //     if (GUI.Button(new Rect(0f, 0f, 100f, 50f), "ReSet"))
    //     {
    //         Distance = 50f;
    //         Target.transform.rotation = Quaternion.identity;
    //     }
    // }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                XAngle += Input.GetAxis("Mouse X") * XSpeed * 0.02f;
                YAngle -= Input.GetAxis("Mouse Y") * YSpeed * 0.02f;
            }
        }


        /*
        if (Input.touchCount > 1)
        {
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                Vec2Pos1 = Input.GetTouch(0).position;
                Vec2Pos2 = Input.GetTouch(1).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                var tempPosition1 = Input.GetTouch(0).position;
                var tempPosition2 = Input.GetTouch(1).position;


                float deltaDis = DeltaDistance(Vec2Pos1, Vec2Pos2, tempPosition1, tempPosition2);
                // 如果在扩大
                if (deltaDis < 0f)
                    Distance += deltaDis * DisSpeed;
                else
                    Distance += deltaDis * DisSpeed;

                // 记录旧的位置
                Vec2Pos1 = tempPosition1;
                Vec2Pos2 = tempPosition2;
            }
        }
        */

    }

    void LateUpdate()
    {
        // 旋转的更新
        ClampXY();
        Target.transform.Rotate(-YAngle, -XAngle, 0f, Space.World);
        XAngle = 0f;
        YAngle = 0f;

        // 摄像机位置的更新
        //ClampDistance();
        //Vector3 cameraPos = MainCamera.transform.position;
        //cameraPos.z = -Distance;
        //MainCamera.transform.position = cameraPos;
    }

    bool IsEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        float leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        float leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
        if (leng1 < leng2)
            return true;
        else
            return false;
    }

    float DeltaDistance(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        float leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        float leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
        return leng1 - leng2;
    }

    void ClampXY()
    {
        if (XAngle < -360)
            XAngle += 360;
        if (XAngle > 360)
            XAngle -= 360;
        if (YAngle < -360)
            YAngle += 360;
        if (YAngle > 360)
            YAngle -= 360;
    }

    void ClampDistance()
    {
        Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);
    }

}