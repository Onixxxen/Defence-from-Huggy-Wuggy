using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    [SerializeField] private List<Transform> _targetPoints = new List<Transform>();

    public List<Transform> TargetPoints => _targetPoints;
}
