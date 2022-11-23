using System.Collections;
using UnityEngine;

namespace Assets.Script
{
    public class RoadBlock : MonoBehaviour
    {
        public bool Fetch(float x)
        {
            return x > transform.position.x + 100f;
        }

        public void Delete()
        {
            Destroy(gameObject);
        }

        private void Start()
        {

        }

        private void Update()
        {

        }
    }
}