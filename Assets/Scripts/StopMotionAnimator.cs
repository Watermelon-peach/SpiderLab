using UnityEngine;

namespace SpiderVerse
{

    [RequireComponent(typeof(Animator))]
    public class StopMotionAnimator : MonoBehaviour
    {
        [SerializeField] private float animationFPS = 12f;

        private Animator animator;

        private float accumulatedTime;
        private float sampledTime;

        private int currentStateHash;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            animator.speed = 0f;

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            currentStateHash = stateInfo.fullPathHash;
        }

        private void Update()
        {
            accumulatedTime += Time.deltaTime;

            float frameDuration = 1f / animationFPS;

            while (accumulatedTime >= frameDuration)
            {
                accumulatedTime -= frameDuration;
                sampledTime += frameDuration;
            }

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.fullPathHash != currentStateHash)
            {
                currentStateHash = stateInfo.fullPathHash;
                sampledTime = 0f;
            }

            animator.Play(currentStateHash, 0, sampledTime / stateInfo.length);
            animator.Update(0f);
        }

        public void SetFPS(float fps)
        {
            animationFPS = Mathf.Max(1f, fps);
        }
    }

}
