using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Listens for touch events and performs an AR raycast from the screen touch point.
    /// AR raycasts will only hit detected trackables like feature points and planes.
    ///
    /// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
    /// and moved to the hit position.
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class PlaceOnPlane : PressInputBase
    {
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject m_PlacedPrefab;

        /// <summary>
        /// The prefab to instantiate on touch.
        /// </summary>
        public GameObject placedPrefab
        {
            get { return m_PlacedPrefab; }
            set { m_PlacedPrefab = value; }
        }

        /// <summary>
        /// The object instantiated as a result of a successful raycast intersection with a plane.
        /// </summary>
        public GameObject spawnedObject { get; private set; }

        bool m_Pressed;

        protected override void Awake()
        {
            base.Awake();
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

        //������ת���Ƕ�����
        public int yMinLimit = -20;
        public int yMaxLimit = 80;
        //��ת�ٶ�
        public float xSpeed = 250.0f;//������ת�ٶ�
        public float ySpeed = 120.0f;//������ת�ٶ�
                                     //��ת�Ƕ�
        private float x = 0.0f;
        private float y = 0.0f;


        void Update()
        {

            if (Pointer.current == null || m_Pressed == false)
                return;

            var touchPosition = Pointer.current.position.ReadValue();


            if(spawnedObject == null)
            {
                if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = s_Hits[0].pose;
                    hitPose.position = new Vector3(hitPose.position.x,hitPose.position.y + 0.85f,hitPose.position.z);
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                    spawnedObject.GetComponent<SmoothController>().MainCamera = GameObject.Find("XR Origin/Camera Offset/Main Camera");
                }
            }
            //if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            //{
            //    // Raycast hits are sorted by distance, so the first one
            //    // will be the closest hit.
            //    var hitPose = s_Hits[0].pose;
                
            //    if (spawnedObject == null)
            //    {
            //        spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
            //    }
            //    else
            //    {
            //        //if(Pointer.current.touchCount == 1)
            //        //{
            //            //spawnedObject.transform.position = hitPose.position;
            //            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            //            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            //            y = ClampAngle(y, yMinLimit, yMaxLimit);
            //            //ŷ����ת��Ϊ��Ԫ��
            //            Quaternion rotation = Quaternion.Euler(y, x, 0);
            //            spawnedObject.transform.rotation = rotation;
            //        //}else if(Pointer.current.touchCount == 2)
            //        //{
            //        //    Debug.Log("ˬֱ");
            //        //}

            //    }
            //}

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

        protected override void OnPress(Vector3 position) => m_Pressed = true;

        protected override void OnPressCancel() => m_Pressed = false;

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;
    }
}
