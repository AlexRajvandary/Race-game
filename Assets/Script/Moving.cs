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

        public GameObject BrokenPrefab { get; set; }

        public GameObject Car { get; set; }

        public Controls Controls { get; set; }

        public GameObject ModelHolder { get; set; }

        public Rigidbody RigidBody { get; set; }

        public List<GameObject> Wheels { get; set; }

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

                if (Wheels.Count > 0)
                {
                    foreach (var wheel in Wheels)
                    {
                        wheel.transform.Rotate(-3f, 0f, 0f);
                    }
                }

                if (tag == "Car")
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