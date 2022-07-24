using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myrotate : MonoBehaviour
{
    //������ת���Ƕ�����
    public int yMinLimit = -20;
    public int yMaxLimit = 80;
    //��ת�ٶ�
    public float xSpeed = 250.0f;//������ת�ٶ�
    public float ySpeed = 120.0f;//������ת�ٶ�
    //��ת�Ƕ�
    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButton(0) /*&& Input.touchCount == 1*/)
        {
            //Input.GetAxis("MouseX")��ȡ����ƶ���X��ľ���
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            //ŷ����ת��Ϊ��Ԫ��
            Quaternion rotation = Quaternion.Euler(y, 0, 0);
            transform.rotation = rotation;
        }
    }

    //�Ƕȷ�Χֵ�޶�
    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}