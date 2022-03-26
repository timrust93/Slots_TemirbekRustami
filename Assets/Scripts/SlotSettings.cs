using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlotSettings : MonoBehaviour
{
    [SerializeField] private Ease _appearTweenEase;
    public Ease AppearTweenEase { get { return _appearTweenEase; } }

    public static SlotSettings Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
