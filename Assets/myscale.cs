using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myscale : MonoBehaviour
{
    public Transform m_zoom;
    private float max = 3.7f;
    private float min = 0;
    protected static float current = 0;
    private float last = -1;

    // Start is called before the first frame update
    void Start()
    {
        //m_zoom = GameObject.Find("XR Origin/Camera Offset/Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {


        //m_zoom.position += m_zoom.forward.normalized * 1;
       //m_zoom.localScale = new Vector3(1,1,1);
        
        if (Input.touchCount == 2)
        {
            float dis = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
            if (-1 == last) last = dis;
            float result = dis - last;
            if (result + current < min)
                result = min - current;
            else if (result + current > max)
                result = max - current;
            result *= 0.1f;
            //m_zoom.localScale = m_zoom.forward.normalized * result;
            m_zoom.localScale = new Vector3(m_zoom.localScale.x+result,m_zoom.localScale.y+result,m_zoom.localScale.z+result);
            current += result;
            last = dis;
        }
        else
        {
            last = -1;
        }
        

    }
}