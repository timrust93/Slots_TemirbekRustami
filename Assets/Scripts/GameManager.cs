using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<Slot> _slots;
    [SerializeField] private List<float> _slotAppearDelays;
    [SerializeField] private float _slotMoveTime;
    [SerializeField] private float _compareObjectFirstDelayTime;
    [SerializeField] private float _nextCompareObjectDelayTime;
    [SerializeField] private List<CompareObject> _compareObjects;
    [SerializeField] private float _proximity; // maximum proximity from element to slot that enables destroy of element
    public float Proximity { get { return _proximity; } }
    [SerializeField] private GameObject _gameFinishedPanel;

    private int _currentCompareObjectIndex = 0;

    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        StartCoroutine(PutSlotsOnPositionsCrtn());
    }

    private void Initialize()
    {
        _compareObjects.Shuffle();
        _slots.Shuffle();

        for (int i = 0; i < _slots.Count; i++)
        {
            Vector2 pos = Layout.Instance.GetInstantiatePositionForSlot(i);
            _slots[i].transform.position = pos;
        }
    }

    public void SlotMatched()
    {
        if (_currentCompareObjectIndex < _compareObjects.Count)
        {
            Sequence s = DOTween.Sequence();
            s.AppendInterval(_nextCompareObjectDelayTime);
            s.AppendCallback(() =>
            {
                CreateCurrentCompareObject();
            });
        }
        else
        {
            LevelFinished();
        }
    }

    public void ReplayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    private IEnumerator PutSlotsOnPositionsCrtn()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            yield return new WaitForSeconds(_slotAppearDelays[i]);
            Vector2 finalPosition = Layout.Instance.GetPositionForSlot(i);
            _slots[i].transform.DOMove(finalPosition, _slotMoveTime).SetEase(SlotSettings.Instance.AppearTweenEase);
        }
        yield return new WaitForSeconds(_compareObjectFirstDelayTime);
        CreateCurrentCompareObject();
    }

    private CompareObject CreateCurrentCompareObject()
    {
        var toInstantiate = _compareObjects[_currentCompareObjectIndex];
        CompareObject co = Instantiate(toInstantiate) as CompareObject;
        co.transform.position = Layout.Instance.GetInstantiatePositionForMainElement();
        co.Appear();
        _currentCompareObjectIndex += 1;
        return co;
    }

    private void LevelFinished()
    {
        _gameFinishedPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
