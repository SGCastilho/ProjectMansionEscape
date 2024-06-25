using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public sealed class PlayerUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject _playerHUDGroup;
        [Space(12)]
        [SerializeField] private Image _playerHealthBar;
        [SerializeField] private Image _playerStaminaBar;
        [SerializeField] private GameObject _playerHealthGroup;

        public void HideHUD(bool hide)
        {
            if(hide)
            {
                _playerHUDGroup.SetActive(false);
            }
            else
            {
                _playerHUDGroup.SetActive(true);
            }
        }

        public void HideBarsHUD(bool hide)
        {
            if(hide)
            {
                _playerHealthGroup.SetActive(false);
            }
            else
            {
                _playerHealthGroup.SetActive(true);
            }
        }

        public void RefreshHealthBar(int currentHealth, int maxHealth)
        {
            float healthInPercentage = (float)currentHealth / maxHealth;

            Debug.Log(healthInPercentage);

            _playerHealthBar.fillAmount = healthInPercentage;
        }

        /*
        public void RefreshStaminaBar(float currentStamina, float maxStamina)
        {
            float staminaInPercentage = currentStamina / maxStamina;

            _playerStaminaBar.fillAmount = staminaInPercentage;

            if(staminaInPercentage >= 1f)
            {
                _playerStaminaBar.gameObject.SetActive(false);
            }
            else if(staminaInPercentage < 1f)
            {
                _playerStaminaBar.gameObject.SetActive(true);
            }

            Debug.Log(staminaInPercentage);
        }
        */
    }
}
