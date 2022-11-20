using System.Collections;
using UnityEngine;

namespace Assets.Script
{
    public class Controls : MonoBehaviour
    {
        public float SideSpeed { get; set; } = 0f;

        public float Speed { get; set; } = 0f;

        public float MaxSpeed { get; set; } = 0.5f;
        
        private void Start()
        {

        }

        private void Update()
        {
            var moveSide = Input.GetAxis("Horizontal");
            var moveForward = Input.GetAxis("Vertical");

            if (moveSide != 0)
            {
                SideSpeed = moveSide * -1f;
            }

            if (moveForward != 0)
            {
                Speed += 0.01f * moveForward;
            }
            else
            {
                if (Speed > 0)
                {
                    Speed -= 0.01f;
                }
                else
                {
                    Speed += 0.01f;
                }
            }

            if (Speed > MaxSpeed)
            {
                Speed = MaxSpeed;
            }
        }
    }
}