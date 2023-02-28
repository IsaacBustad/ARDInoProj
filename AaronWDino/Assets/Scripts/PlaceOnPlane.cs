using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
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
    public class PlaceOnPlane : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]

        //GameObject[] placedPrefabArray;
        //int arrayIndex;

        GameObject m_PlacedPrefab;

        //Vector3 spawnedPositionInAR;
        //Quaternion spawnedRotationInAR;

        /// <summary>
        /// The prefab to instantiate on touch.
        /// </summary>

        //public GameObject[] PlacedPrefabArray
        //{
        //    get { return placedPrefabArray; }
        //    set { placedPrefabArray = value; }
        //}
        public GameObject placedPrefab
        {
            get { return m_PlacedPrefab; }
            set { m_PlacedPrefab = value; }
        }

        /// <summary>
        /// The object instantiated as a result of a successful raycast intersection with a plane.
        /// </summary>
        public GameObject spawnedObject { get; private set; }

        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();

            //checks if tehre's a stored preference for prefab index
            //if (!PlayerPrefs.HasKey("myPreferredPrefab"))
            //    PlayerPrefs.SetInt("myPreferredPrefab", 0);

            //arrayIndex = PlayerPrefs.GetInt("myPreferredPrefab");
            //placedPrefab = placedPrefabArray[arrayIndex];
        }

        //public void NextPrefabInArray()
        //{
        //    if (arrayIndex == PlacedPrefabArray.Length - 1)
        //        arrayIndex = 0;
        //    else
        //        arrayIndex += 1;
        //    //PlayerPrefs.SetInt("myPreferredPrefab", arrayIndex);
        //    placedPrefab = PlacedPrefabArray[arrayIndex];
        //    RespawnSpawnedObject();
        //}
        //public void PreviousPrefabInArray()
        //{
        //    if (arrayIndex == 0)
        //        arrayIndex = PlacedPrefabArray.Length - 1;
        //    else
        //        arrayIndex -= 1;
        //    //PlayerPrefs.SetInt("myPreferredPrefab", arrayIndex);
        //    placedPrefab = PlacedPrefabArray[arrayIndex];
        //    RespawnSpawnedObject();
        //}

        //void RespawnSpawnedObject()
        //{
        //    if (spawnedObject == null)
        //    {
        //        spawnedPositionInAR = spawnedObject.gameObject.transform.position;
        //        spawnedRotationInAR = spawnedObject.gameObject.transform.rotation;
        //        spawnedObject = null;
        //        spawnedObject = Instantiate(m_PlacedPrefab, spawnedPositionInAR, spawnedRotationInAR);
        //    }
        //}

        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }

            touchPosition = default;
            return false;
        }

        void Update()
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnedObject.transform.position = hitPose.position;
                }
            }
        }

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;


        public void TogglePlaceOnPlane()
        {
            enabled = !enabled;
        }
        public void TurnOffPlaceOnPlane()
        {
            enabled = false;
        }
        public void TurnOnPlaceOnPlane()
        {
            enabled = true;
        }
        public void ResetDino()
        {
            Destroy(spawnedObject);
        }
    }
}
