using System;
using ProjectLumina.Capabilities;
using ProjectLumina.Controllers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Data
{
    [Serializable]
    public class LootItem
    {
        public Pickupable pickupable;

        [Range(0, 10)]
        public float dropWeight;

        [Range(0, 100)]
        public int amount;
    }

    [Serializable, HideLabel]
    public class LootPool
    {
        public LootItem[] lootItems;

        public float[] Weights
        {
            get
            {
                float[] weights = new float[lootItems.Length];
                for (int i = 0; i < lootItems.Length; i++)
                {
                    weights[i] = lootItems[i].dropWeight;
                }
                return weights;
            }
        }

        public float SumWeight
        {
            get
            {
                float sumWeight = 0;
                foreach (var rarity in lootItems)
                {
                    sumWeight += rarity.dropWeight;
                }
                return sumWeight;
            }
        }

        public LootItem GetLootItem()
        {
            float randomWeight = UnityEngine.Random.Range(0, SumWeight);
            for (int i = 0; i < Weights.Length; ++i)
            {
                randomWeight -= Weights[i];
                if (randomWeight < 0)
                {
                    return lootItems[i];
                }
            }

            return null;
        }
    }

    [AddComponentMenu("Data/Loot")]
    public class Loot : MonoBehaviour
    {
        [BoxGroup("Loot"), SerializeField]
        private LootPool _loot;

        [BoxGroup("Drop Position"), SerializeField]
        private Vector2 _minPosition, _maxPosition;

        [Space]
        public UnityEvent OnLootDrop;

        public void DropLoot()
        {
            for (int i = 0; i < _loot.lootItems.Length; i++)
            {
                float randomXPosition = UnityEngine.Random.Range(_minPosition.x + transform.position.x, _maxPosition.x + transform.position.x);
                float randomYPosition = UnityEngine.Random.Range(_minPosition.y + transform.position.y, _maxPosition.y + transform.position.y);
                Vector3 randomSpawnPosition = new(randomXPosition, randomYPosition, 0);
                ObjectPoolController.Instance.GetPooledObject(_loot.GetLootItem().pickupable.name, randomSpawnPosition, true);
            }

            OnLootDrop?.Invoke();
        }
    }
}