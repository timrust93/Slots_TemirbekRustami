using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private ObjectType _objectType;
    public ObjectType ObjectType { get { return _objectType; } }
}
