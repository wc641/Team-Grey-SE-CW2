using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VS.CW2RTS.Units.Player;
using VS.CW2RTS.Player;

namespace VS.CW2RTS.Units
{
    public class UnitStatDisplay : MonoBehaviour
    {
        public float maxHealth, armour, currentHealth;
        public GameOverWinScreen GameOverWinScreen;

        [SerializeField] private Image healthBarAmount;

        private bool isPlayerUnit = false;

        public void SetStatDisplayBasicUnit(UnitStatTypes.Base stats, bool isPlayer)
        {
            maxHealth = stats.health;
            armour = stats.armour;
            isPlayerUnit = isPlayer;

            currentHealth = maxHealth;
        }

        public void SetStatDisplayBasicBuilding(Buildings.BuildingStatTypes.Base stats, bool isPlayer)
        {
            maxHealth = stats.health;
            armour = stats.armour;
            isPlayerUnit = isPlayer;

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

        public void Heal(float healAmount)
        {
            currentHealth += healAmount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
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
                PlayerManager.numberOfplayerUnits = PlayerManager.numberOfplayerUnits - 1;
                Destroy(gameObject.transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject.transform.parent.gameObject);
                if (gameObject.transform.parent.gameObject.name.Equals("enemyCore"))
                {
                    GameOverWinScreen.Setup();
                }
            }
        }
    }

}
