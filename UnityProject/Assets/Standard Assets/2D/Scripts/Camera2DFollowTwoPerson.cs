using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollowTwoPerson : MonoBehaviour
    {
        public Transform target;
        public Transform target2;

        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;


        public float minOrthoSize = 5;
        public float borderDistance = 2f;
        public float orthoDamping = 1;
        public float orthoLookAheadFactor = 3;
        public float orthoLookAheadReturnSpeed = 0.5f;
        public float orthoLookAheadMoveThreshold = 0.1f;
        private float m_OrthoLastTargetValue;
        private float m_OrthoCurrentVelocity;
        private float m_OrthoLookAheadPos;

        private Camera m_camera;


        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        // Use this for initialization
        private void Start()
        {
            m_camera = GetComponent<Camera>();
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            m_camera = GetComponent<Camera>();
            transform.parent = null;
        }

        public Vector3 TargetPosition
        {
            get { return 0.5f * (target.position + target2.position); }
        }

        public Vector3 ManhattenDistance
        {
            get
            {
                Vector3 diff = target.position - target2.position;
                return new Vector3(Mathf.Abs(diff.x), Mathf.Abs(diff.y)); }
        }

        // Update is called once per frame
        private void Update()
        {
            // only update lookahead pos if accelerating or changed direction
            //Vector3 pos = 0.5f * (target.position + target2.position);
            float xMoveDelta = (TargetPosition - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = TargetPosition + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = TargetPosition;


            /*
            float maxVal = ManhattenDistance.x > ManhattenDistance.y ? ManhattenDistance.x : ManhattenDistance.y;
            m_camera.orthographicSize = Mathf.Max(minOrthoSize, maxVal);

            Debug.Log(Mathf.Max(minOrthoSize, maxVal));

            C*/

            /*
            float distToBorder1X = m_camera.aspect * m_camera.orthographicSize - Mathf.Abs(target.position.x - transform.position.x);
            float distToBorder1Y = m_camera.orthographicSize - Mathf.Abs(target.position.y - transform.position.y);

            float distToBorder2X = m_camera.aspect * m_camera.orthographicSize - Mathf.Abs(target2.position.x - transform.position.x);
            float distToBorder2Y = m_camera.orthographicSize - Mathf.Abs(target2.position.y - transform.position.y);


            float maxDist = Mathf.Max(distToBorder1X, distToBorder1Y, distToBorder2X, distToBorder2Y);
            */

            float distToBorder1X = ManhattenDistance.x / m_camera.aspect;
            float distToBorder1Y = ManhattenDistance.y;
            float newOrtho = 0.5f * Mathf.Max(distToBorder1X, distToBorder1Y) + borderDistance;
            newOrtho = Mathf.Max(minOrthoSize, newOrtho);

            //m_camera.orthographicSize = Mathf.Max(minOrthoSize, newOrtho);
            Debug.Log(new Vector3(distToBorder1X, distToBorder1Y));


            float orthoMoveDelta = newOrtho - m_OrthoLastTargetValue;

            bool orthoUpdateLookAheadTarget = Mathf.Abs(orthoMoveDelta) > orthoLookAheadMoveThreshold;

            if (orthoUpdateLookAheadTarget)
            {
                m_OrthoLookAheadPos = orthoLookAheadFactor * Mathf.Sign(orthoMoveDelta);
            }
            else
            {
                m_OrthoLookAheadPos = Mathf.MoveTowards(m_OrthoLookAheadPos, 0, Time.deltaTime * orthoLookAheadReturnSpeed);
            }

            float orthoAheadTargetPos = newOrtho + m_OrthoLookAheadPos;
            float orthoNewPos = Mathf.SmoothDamp(m_camera.orthographicSize, orthoAheadTargetPos, ref m_OrthoCurrentVelocity, orthoDamping);

            m_camera.orthographicSize = orthoNewPos;

            m_LastTargetPosition = TargetPosition;


        }
    }
}
