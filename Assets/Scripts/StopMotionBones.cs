using UnityEngine;

namespace SpiderVerse
{
    public class StopMotionBones : MonoBehaviour
    {
        [SerializeField] private float animationFPS = 12f;

        private Transform[] bones;
        private Vector3[] positions;
        private Quaternion[] rotations;

        private float timer;

        void Start()
        {
            bones = GetComponentsInChildren<Transform>();

            positions = new Vector3[bones.Length];
            rotations = new Quaternion[bones.Length];

            CapturePose();
        }

        void LateUpdate()
        {
            timer += Time.deltaTime;

            float frameTime = 1f / animationFPS;

            if (timer >= frameTime)
            {
                timer = 0f;
                CapturePose();
            }
            else
            {
                ApplyPose();
            }
        }

        void CapturePose()
        {
            for (int i = 0; i < bones.Length; i++)
            {
                positions[i] = bones[i].localPosition;
                rotations[i] = bones[i].localRotation;
            }
        }

        void ApplyPose()
        {
            for (int i = 0; i < bones.Length; i++)
            {
                bones[i].localPosition = positions[i];
                bones[i].localRotation = rotations[i];
            }
        }
    }

}
