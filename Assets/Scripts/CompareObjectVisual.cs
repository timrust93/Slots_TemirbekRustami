using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CompareObjectVisual : MonoBehaviour
{
    private Vector3 _normalScale;

    private Tween selectTween;

    public void SelectAnim()
    {
        selectTween?.Kill();
        selectTween = transform.DOScale(_normalScale * COS.Instance.ZoomedScalePerc, COS.Instance.ZoomTime);
    }

    public void DeselectAnim()
    {
        selectTween?.Kill();
        selectTween = transform.DOScale(_normalScale, COS.Instance.ZoomTime);
    }


    // Start is called before the first frame update
    void Start()
    {
        _normalScale = transform.localScale;
    }
}
