using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script
{
    public class Moving : MonoBehaviour
    {
        private float speed = 0.1f;
        private float maxSpeed = 0.5f;
        private float minSpeed = 0.1f;
        private bool isAlive = true;
        private bool isKilled = false;

        public GameObject BrokenPrefab;
        public GameObject Car;
        public Controls Controls;
        public GameObject ModelHolder;
        public Rigidbody RigidBody;
        public List<GameObject> Wheels;

        private void Start()
        {
            RigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (isAlive)
            {
                var newSpeed = speed;
                var sideSpeed = 0f;

                if (Controls != null)
                {
                    newSpeed += Controls.Speed;
                    sideSpeed = Controls.SideSpeed;
                }

                if (newSpeed > maxSpeed)
                {
                    newSpeed = maxSpeed;
                }

                if (newSpeed < minSpeed)
                {
                    newSpeed = minSpeed;
                }

                transform.position = new Vector3(transform.position.x + newSpeed, transform.position.y, transform.position.z + 0.1f * sideSpeed);

                if (Controls != null)
                {
                    Controls.SideSpeed = 0f;
                }

                if (Wheels?.Count > 0)
                {
                    foreach (var wheel in Wheels)
                    {
                        wheel.transform.Rotate(-3f, 0f, 0f);
                    }
                }

                if (CompareTag("Car"))
                {
                    if (transform.position.y < -50f)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}