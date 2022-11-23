using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Script
{
    public class Road : MonoBehaviour
    {
        public List<GameObject> blocks;
        public GameObject carPrefab;
        public GameObject coinPrefab;
        public GameObject player;
        public GameObject roadPrefab;

        private System.Random rand = new System.Random();
        private float step = 24.69f;
        private float lastBlockPositionX;
        private float roadY;
        private float roadZ;
        private ObjectPool<GameObject> pool;

        public Road()
        {
            pool = new ObjectPool<GameObject>(CreateRoadBlock, GetRoadBlock, RealiseRoadBlock, defaultCapacity: 100);
        }

        private void Update()
        {
            var x = player.GetComponent<Moving>().RigidBody.position.x;

            var last = blocks[^1];
            lastBlockPositionX = last.transform.position.x;

            if (x > last.transform.position.x - step * 10f)
            {
                pool.Get(out var block);
                blocks.Add(block);
            }

            var blocksToRemove = new List<GameObject>();

            foreach (var block in blocks)
            {
                if (block.GetComponent<RoadBlock>().Fetch(x))
                {
                    blocksToRemove.Add(block);
                }
            }

            for (int i = 0; i < blocksToRemove.Count; i++)
            {
                blocks.Remove(blocksToRemove[i]);
                pool.Release(blocksToRemove[i]);
            }
        }

        private void Start()
        {
            var block = blocks.LastOrDefault();
            if (block != null)
            {
                roadY = block.transform.position.y;
                roadZ = block.transform.position.z;
            }
        }

        private GameObject CreateRoadBlock()
        {
            var block = Instantiate(roadPrefab);
            block.transform.SetPositionAndRotation(GetNextBlockPosition(lastBlockPositionX), Quaternion.identity);
            block.transform.SetParent(gameObject.transform);
            return block;
        }

        private void GetRoadBlock(GameObject gameObject)
        {
            gameObject.transform.SetPositionAndRotation(GetNextBlockPosition(lastBlockPositionX), Quaternion.identity);
            gameObject.SetActive(true);
        }

        private void RealiseRoadBlock(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        private Vector3 GetNextBlockPosition(float lastBlockPositionX)
        {
            return new Vector3(lastBlockPositionX + step,
                                roadY,
                                roadZ);
        }
    }
}