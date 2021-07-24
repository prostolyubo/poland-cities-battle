using System;
using UnityEngine;
using UnityEngine.Events;

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

        public bool waitForSignal;
        Action fire;
        public Animation signaller;

        void Awake()
        {
            controller.OnUseTriggered += Shoot;
        }

        public void Fire()
        {
            fire?.Invoke();
        }

        internal void Shoot(GameObject prefab)
        {
            fire = () =>
            {
                GameObject projectile = Instantiate(prefab);
                projectile.transform.localScale = flipReference.localScale;
                projectile.transform.position = spawnPoint.position;
                var direction = Quaternion.Euler(0, 0, flipReference.localScale.x > 0 ? angle : -angle);
                var force = direction * (flipReference.localScale.x * spawnPoint.right) * shootForce;
                projectile.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            };
            if (!waitForSignal)
                Fire();
            else
                signaller?.Play();
        }

        private void Shoot(Action obj)
        {
            if (stamina.CurrentStamina < staminaCost)
            {
                obj?.Invoke();
                return;
            }
            stamina.CurrentStamina -= staminaCost;
            Shoot(prefab);
            stamina.StartReplenishing();
        }
    }
}
