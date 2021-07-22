using System;
using UnityEngine;

namespace Assets
{
    class ProjectileActor : MonoBehaviour
    {
        public WeaponController controller;
        public GameObject prefab;
        public Transform spawnPoint;
        public Transform flipReference;
        public Stamina stamina;

        [Range(-90,90)]
        public float angle;
        public float shootForce;
        public float staminaCost;

        void Awake()
        {
            controller.OnUseTriggered += Shoot;
        }

        internal void Shoot(GameObject projectile)
        {
            projectile.transform.position = spawnPoint.position;
            var direction = Quaternion.Euler(0, 0, flipReference.localScale.x > 0 ? angle : -angle);
            var force = direction * (flipReference.localScale.x * spawnPoint.right) * shootForce;
            projectile.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }

        private void Shoot(Action obj)
        {
            if (stamina.CurrentStamina < staminaCost)
                return;
            stamina.CurrentStamina -= staminaCost;
            Shoot(Instantiate(prefab));
            stamina.StartReplenishing();
        }
    }
}
