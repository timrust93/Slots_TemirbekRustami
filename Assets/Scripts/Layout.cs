using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Layout : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private List<RectTransform> _layoutSlotGameObjects; // Just to make layout easier UI is used
    [SerializeField] private RectTransform _layoutMainElement;

    [SerializeField] private RectTransform _yForSlotInstantiateLayoutRt;
    [SerializeField] private RectTransform _xForMainElementInstantiateLayoutRt;

    public static Layout Instance;
    private void Awake()
    {
        Instance = this;
        Canvas.ForceUpdateCanvases();
        _canvasGroup.alpha = 0;
    }

    public Vector2 PositionForMainElement => _layoutMainElement.transform.position;
    public Vector2 GetPositionForSlot(int index)
    {
        //return _positionsForSlots[index];
        return _layoutSlotGameObjects[index].transform.position;
    }

    public Vector2 GetInstantiatePositionForSlot(int index)
    {
        float x = _layoutSlotGameObjects[index].transform.position.x;
        float y = _yForSlotInstantiateLayoutRt.transform.position.y;
        return new Vector2(x, y);
    }
    public Vector2 GetInstantiatePositionForMainElement()
    {
        float x = _xForMainElementInstantiateLayoutRt.position.x;
        float y = _layoutMainElement.transform.position.y;
        return new Vector2(x, y);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
