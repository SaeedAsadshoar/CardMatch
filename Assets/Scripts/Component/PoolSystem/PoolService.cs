using System.Collections.Generic;
using UnityEngine;

namespace Component.PoolSystem
{
    public static class PoolService
    {
        private static Dictionary<int, List<GameObject>> _pool = new Dictionary<int, List<GameObject>>();

        private static Transform _root;

        public static void AddObjectToPool(int itemId, GameObject gameObject)
        {
            if (_root == null)
            {
                _root = new GameObject("ObjectPool").transform;
            }

            if (!_pool.ContainsKey(itemId))
            {
                GameObject ins = Object.Instantiate(gameObject, _root, true);
                ins.name = gameObject.name;
                ins.SetActive(false);
                _pool.Add(itemId, new List<GameObject>());
                _pool[itemId].Add(ins);

                AddItem(itemId);
            }
        }

        public static GameObject GetItem(int itemId)
        {
            if (!_pool.ContainsKey(itemId))
            {
                Debug.LogError("please use AddObjectToPool method to add first item to pool");
                return null;
            }

            var list = _pool[itemId];
            int count = list.Count;
            for (int i = 1; i < count; i++)
            {
                if (!list[i].activeInHierarchy)
                {
                    list[i].SetActive(true);
                    return list[i];
                }
            }

            var item = AddItem(itemId);
            item.SetActive(true);
            return item;
        }

        private static GameObject AddItem(int itemId)
        {
            var item = _pool[itemId][0];
            GameObject ins = Object.Instantiate(item, _root, true);
            ins.name = $"{item.name}_{_pool[itemId].Count}";
            ins.SetActive(false);
            _pool[itemId].Add(ins);
            return ins;
        }

        public static void BackToPool(GameObject gameObject)
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(_root);
            gameObject.transform.localScale = Vector3.one;
        }
    }
}