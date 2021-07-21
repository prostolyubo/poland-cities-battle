using System;
using UnityEngine;

namespace Assets
{
    class ProjectileActor : MonoBehaviour
    {
        public WeaponController controller;
        public Projectile prefab;
        public Transform spawnPoint;
        public Transform flipReference;

        [Range(-90,90)]
        public float angle;

        void Awake()
        {
            controller.OnUseTriggered += Shoot;
        }
        private void Shoot(Action obj)
        {
            Instantiate(prefab).Shoot(this);
        }
    }
}
