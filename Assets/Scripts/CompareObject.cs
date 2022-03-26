using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CompareObject : MonoBehaviour
{
    [SerializeField] private ObjectType _objectType;
    public ObjectType ObjectType { get { return _objectType; } }

    private Slot _slotOnWhichIam;
    private FingerMover _fingerMover;
    private CompareObjectVisual _myVisual;

    private bool _touchRestricted = true;

    private void Awake()
    {
        _myVisual = GetComponent<CompareObjectVisual>();
        _fingerMover = GetComponent<FingerMover>();
    }

    public void Appear()
    {
        SoundManager.instance?.PlaySoundByType(SoundName.ItemAppear);
        transform.DOMove(Layout.Instance.PositionForMainElement, COS.Instance.AppearTime).SetEase(COS.Instance.AppearTweenEase).OnComplete(() =>
        {
            _touchRestricted = false;
        });
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Slot s = col.gameObject.GetComponent<Slot>();
        if (s != null)
        {
            _slotOnWhichIam = s;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _slotOnWhichIam = null;
    }

    private void OnMouseDown()
    {
        if (_touchRestricted)
            return;
        _myVisual.SelectAnim();
        _fingerMover.SetMoveAllowed(true);
    }

    private void OnMouseUp()
    {
        _myVisual.DeselectAnim();
        _fingerMover.SetMoveAllowed(false);
        _touchRestricted = true;

        if (CheckMatch() && CheckProximity())
        {
            MakeMatch();
        }
        else
        {
            SnapToInitialPos();
        }
    }

    private void SnapToInitialPos()
    {
        float distance = Vector2.Distance(transform.position, Layout.Instance.PositionForMainElement);
        transform.DOMove(Layout.Instance.PositionForMainElement, distance / COS.Instance.SnapOnPosSpeed).OnComplete(() => _touchRestricted = false);
    }

    private void MakeMatch()
    {
        SoundManager.instance?.PlaySoundByType(SoundName.ItemSuccess);
        transform.DOMove(_slotOnWhichIam.transform.position, COS.Instance.SnapOnSlotTime).OnComplete(() =>
        {
            GameManager.Instance.SlotMatched();
        });
    }

    private bool CheckProximity()
    {
        if (Vector2.Distance(transform.position, _slotOnWhichIam.transform.position) < GameManager.Instance.Proximity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckMatch()
    {
        if (_slotOnWhichIam != null && _slotOnWhichIam.ObjectType == this.ObjectType)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
