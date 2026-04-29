using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Lets the player drag the icon of a filled inventory slot onto 
/// another slot to swap items
/// </summary>
[RequireComponent(typeof(ItemSlot), typeof(CanvasGroup), typeof(RectTransform))]
public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ItemSlot      _sourceSlot;
    private CanvasGroup   _canvasGroup;
    private RectTransform _rect;
    private Vector2 _homePos;

    private void Awake()
    {
        _sourceSlot  = GetComponent<ItemSlot>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _rect        = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Called automatically by Unity's EventSystem when a drag begins
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Empty slots cannot be dragged
        if (_sourceSlot.currentItem == null)
        {
            return;
        }
        _homePos = _rect.anchoredPosition;
        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Empty slots cannot be dragged
        if (_sourceSlot.currentItem == null)
        {
            return;
        }

        Canvas canvas = GetComponentInParent<Canvas>();
        float  scale  = canvas != null ? canvas.scaleFactor : 1f;
        _rect.anchoredPosition += eventData.delta / scale;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true; //enable raycasts
        _canvasGroup.alpha = 1f; //no longer transparent
        _rect.anchoredPosition      = _homePos;

        if (_sourceSlot.currentItem == null)
        {
            return;
        }

        ItemSlot dropSlot = eventData.pointerEnter != null ? eventData.pointerEnter.GetComponentInParent<ItemSlot>(): null;
        if(dropSlot == null || dropSlot == _sourceSlot)
        {
            return;
        }

        ItemSO originalSlot = dropSlot.currentItem;
        dropSlot.SetItem(_sourceSlot.currentItem);

        if (dropSlot != null)
        {
            _sourceSlot.SetItem(originalSlot);
        }
        else
        {
            _sourceSlot.ClearItem();
        }
    }
}
