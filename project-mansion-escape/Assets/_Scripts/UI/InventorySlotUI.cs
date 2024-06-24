using Core.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public sealed class InventorySlotUI : MonoBehaviour
    {
        #region Encapsulation
        internal int SlotID { get => _slotID; set => _slotID = value; }
        internal ItemData Data { get => _itemData; }
        internal bool IsOccupted { get => _isOccupted; }
        #endregion

        [Header("UI Elements")]
        [SerializeField] private Image _itemImage;
        [SerializeField] private GameObject _itemSpriteGroup;
        
        [Header("Settings")]
        [SerializeField] private int _slotID;
        [SerializeField] private ItemData _itemData;
        [SerializeField] private bool _isOccupted;

        internal void OccupSlot(ItemData data)
        {
            if(_isOccupted) return;

            _itemData = data;
            _isOccupted = true;

            _itemImage.sprite = _itemData.Sprite;
            _itemSpriteGroup.SetActive(true);
        }

        internal void DesoccupSlot()
        {
            _itemData = null;
            _isOccupted = false;

            _itemSpriteGroup.SetActive(false);

            _itemImage.sprite = null;
        }
    }
}
