using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VS.CW2RTS.Units
{
    public class UnitStatDisplay : MonoBehaviour
    {
        public float maxHealth, armour, currentHealth;

        [SerializeField] private Image healthBarAmount;

        private bool isPlayerUnit = false;

        private void Start()
        {
            try
            {
                maxHealth = gameObject.GetComponentInParent<Player.PlayerUnit>().baseStats.health;
                armour = gameObject.GetComponentInParent<Player.PlayerUnit>().baseStats.armour;
                isPlayerUnit = true;
            }
            catch(Exception)
            {
                Debug.Log("No player Unit. Trying Enemy Unit...");
                try
                {
                    maxHealth = gameObject.GetComponentInParent<Enemy.EnemyUnit>().baseStats.health;
                    armour = gameObject.GetComponentInParent<Enemy.EnemyUnit>().baseStats.armour;
                    isPlayerUnit = false;
                }
                catch (Exception)
                {
                    Debug.Log("No Unit Scripts found!");
                }
            }

            currentHealth = maxHealth;
        }

        private void Update()
        {
            HandleHealth();
        }

        public void TakeDamage(float damage)
        {
            float totalDamage = damage - armour;
            currentHealth -= totalDamage;
        }

        private void HandleHealth()
        {
            Camera camera = Camera.main;
            gameObject.transform.LookAt(gameObject.transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);

            healthBarAmount.fillAmount = currentHealth / maxHealth;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isPlayerUnit)
            {
                InputManager.InputHandler.instance.selectedUnits.Remove(gameObject.transform.parent);
                Destroy(gameObject.transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

}
