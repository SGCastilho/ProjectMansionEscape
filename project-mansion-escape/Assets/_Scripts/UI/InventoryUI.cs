using TMPro;
using Core.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public sealed class InventoryUI : MonoBehaviour
    {
        public delegate void UseItem(string itemKey);
        public event UseItem OnUseItem;
        public delegate void EquipItem(string itemKey);
        public event EquipItem OnEquipItem;
        public delegate void UnequipItem();
        public event UnequipItem OnUnequipItem;
        public delegate void DiscardItem(string itemKey);
        public event DiscardItem OnDiscardItem;

        [Header("Settings")]
        [SerializeField] private InventorySlotUI[] _inventorySlots;
        [Space(12)]
        [SerializeField] private GameObject _inventoryGroup;
        [Space(12)]
        [SerializeField] private RectTransform _actionGroupTransform;
        [SerializeField] private float _actionGroupDeadline = -2.30f;
        [SerializeField] private float _actionGroupAdjust = 1.25f;
        [Space(12)]
        [SerializeField] private TextMeshProUGUI _tmpUseEquipButton;
        [Space(12)]
        [SerializeField] private GameObject _examineGroup;
        [SerializeField] private TextMeshProUGUI _tmpExamineItem;
        [Space(12)]
        [SerializeField] private Image _equippedWeaponImage;
        [SerializeField] private TextMeshProUGUI _tmpEquippedWeaponName;
        [SerializeField] private TextMeshProUGUI _tmpEquippedWeaponType;
        [SerializeField] private TextMeshProUGUI _tmpEquippedWeaponDuration;

        private InventorySlotUI _currentSlotClicked;

        private void Awake()
        {
            SetupInventorySlots();
        }

        private void SetupInventorySlots()
        {
            for(int i = 0; i < _inventorySlots.Length; i++)
            {
                _inventorySlots[i].SlotID = i;
            }
        }

        public void OpenInventory()
        {
            _inventoryGroup.SetActive(true);
        }

        public void CloseInventory()
        {
            CloseActionGroup();
            
            _inventoryGroup.SetActive(false);
        }

        public void AddItem(ItemData itemData)
        {
            for(int i = 0; i < _inventorySlots.Length; i++)
            {
                if(!_inventorySlots[i].IsOccupted)
                {
                    _inventorySlots[i].OccupSlot(itemData);
                    break;
                }
            }
        }

        public void OpenActionGroup(RectTransform slotTransform)
        {
            if(!_currentSlotClicked.IsOccupted) return;

            _actionGroupTransform.position = slotTransform.position;

            if(_actionGroupTransform.position.y <= _actionGroupDeadline)
            {
                _actionGroupTransform.position = new Vector2(_actionGroupTransform.position.x, _actionGroupAdjust);
            }

            switch(_currentSlotClicked.Data.Type)
            {
                case ItemType.USABLE:
                    _tmpUseEquipButton.text = "Use";
                    break;
                case ItemType.EQUIPABLE:
                    _tmpUseEquipButton.text = "Equip";
                    break;
            }

            _actionGroupTransform.gameObject.SetActive(true);
        }

        public void CloseActionGroup()
        {
            ClearSlotCliked();

            _actionGroupTransform.gameObject.SetActive(false);
        }

        public void GetSlotClicked(InventorySlotUI slotClicked)
        {
            CloseActionGroup();

            _currentSlotClicked = slotClicked;
        }

        internal void ClearSlotCliked()
        {
            _currentSlotClicked = null;
        }

        public void UseEquipAction()
        {
            if(_currentSlotClicked == null) return;

            switch(_currentSlotClicked.Data.Type)
            {
                case ItemType.USABLE:
                    OnUseItem?.Invoke(_currentSlotClicked.Data.Key);
                    DiscardAction();
                    break;
                case ItemType.EQUIPABLE:
                    OnEquipItem?.Invoke(_currentSlotClicked.Data.Key);
                    SetWeaponPanel();
                    break;
            }

            CloseActionGroup();
        }
        
        public void ExamineAction()
        {
            if(_currentSlotClicked == null) return;

            _tmpExamineItem.text = _currentSlotClicked.Data.Description;

            _examineGroup.SetActive(true);

            CloseActionGroup();
        }

        public void DiscardAction()
        {
            if(_currentSlotClicked == null) return;

            OnDiscardItem?.Invoke(_currentSlotClicked.Data.Key);

            _currentSlotClicked.DesoccupSlot();

            CloseActionGroup();
        }

        internal void SetWeaponPanel()
        {
            if(_currentSlotClicked == null) return;

            _tmpEquippedWeaponName.text = _currentSlotClicked.Data.Name;

            _equippedWeaponImage.sprite = _currentSlotClicked.Data.Sprite;
            _equippedWeaponImage.gameObject.SetActive(true);

            switch(_currentSlotClicked.Data.WeaponToEquip.Type)
            {
                case EquipmentData.WeaponType.MELEE:
                    _tmpEquippedWeaponType.text = "Durability";
                    break;
                case EquipmentData.WeaponType.RANGED:
                    _tmpEquippedWeaponType.text = "Munition";
                    break;
            }

            _tmpEquippedWeaponDuration.text = _currentSlotClicked.Data.WeaponToEquip.Duration.ToString();
        }

        public void RefreshWeaponDuration(int duration)
        {
            _tmpEquippedWeaponDuration.text = duration.ToString();
        }

        public void UnequipAction()
        {
            OnUnequipItem?.Invoke();
            UnequipUI();
        }

        public void UnequipUI()
        {
            _tmpEquippedWeaponName.text = "No Weapon";

            _equippedWeaponImage.sprite = null;
            _equippedWeaponImage.gameObject.SetActive(false);

            _tmpEquippedWeaponType.text = string.Empty;
            _tmpEquippedWeaponDuration.text = string.Empty;
        }
    }
}
