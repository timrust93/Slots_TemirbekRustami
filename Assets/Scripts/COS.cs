using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// Settings for compare objects
public class COS : MonoBehaviour
{
    [SerializeField] private float _appearTime;
    public float AppearTime { get { return _appearTime; } }
    [SerializeField] private float _snapOnPosSpeed;
    public float SnapOnPosSpeed { get { return _snapOnPosSpeed; } }
    [SerializeField] private float _snapOnSlotTime;
    public float SnapOnSlotTime { get { return _snapOnSlotTime; } }

    [SerializeField] private float _zoomTime;
    public float ZoomTime { get { return _zoomTime; } }

    [SerializeField] private float _zoomedScalePerc;
    public float ZoomedScalePerc { get { return _zoomedScalePerc; } }

    [SerializeField] private Ease _appearTweenEase;
    public Ease AppearTweenEase { get { return _appearTweenEase; } }


    public static COS Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
