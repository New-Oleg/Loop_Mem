using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RoadGenerator : MonoBehaviour
{
    [Tooltip("Префаб чарактера"), SerializeField]
    private GameObject Character;

    [Tooltip("Префаб дороги"), SerializeField]
    private GameObject Road;


    [Tooltip("Колличество поворотов, колличество поворотов должно быть четным"),
        Range(4, 1000),
        SerializeField]
    private int TurnCount = 4;

    [Tooltip("Колличество дорог, колличество дорог должно быть четным"),
        Range(4, 1000),
        SerializeField]
    private int RoadCount = 4;

    private Vector2Int _currentPoss = Vector2Int.zero;
    private Vector2Int _PointOnMaxDistance = Vector2Int.zero;
    private Vector2Int _currentRotate = Vector2Int.up;
    private Vector2Int _currentDirection = Vector2Int.up;
    private int _sideCount;
    private int _MaxDistance = 0;
    private int _identicalTurnCount = 0;

    public void Awake()
    {
        _sideCount = RoadCount / TurnCount;
        Generate();
        Instantiate(Character, Vector3.zero, Quaternion.Euler(Vector3.zero), transform);
    }
    
    private void OnEnable()
    {
        ShopController.GenerateNewLevel += UpgreadLevel;
    }
    private void OnDisable()
    {
        ShopController.GenerateNewLevel -= UpgreadLevel;
    }

    private void Generate()
    {
        for (int i = 1; i < TurnCount + 1; i++)
        {
            MakeRoad();
            MakeTurn();
        }
        if (_currentPoss != Vector2Int.zero) CompleateGenerate();

        transform.position = Vector3.zero;
        transform.position = transform.position - (Vector3)GetCenter();
    }

    private void UpgreadLevel()
    {
        transform.position = Vector3.zero;
        DestroyRoad();
        TurnCount += 2;
        RoadCount += 4;
        _sideCount = RoadCount / TurnCount;
        Generate();
    }

    private void DestroyRoad()
    {
        foreach (GameObject T in GameObject.FindGameObjectsWithTag("Tail"))
        {
            Destroy(T);
        }
    }
    private void MakeRoad()
    {
        for (int i = 0; i < _sideCount; i++)
        {
            _currentPoss += 1 * _currentDirection; 
            Instantiate(Road, (Vector3Int) _currentPoss, Quaternion.Euler(0,0, _currentRotate.y), transform);
            if (Math.Abs(_currentPoss.x) + Math.Abs(_currentPoss.y) > _MaxDistance) {
                _MaxDistance = Math.Abs(_currentPoss.x) + Math.Abs(_currentPoss.y);
                _PointOnMaxDistance = _currentPoss;
            }

        }
    }
    private void MakeTurn()
    {
        bool turnDerection = UnityEngine.Random.value >= 0.5f;
        _identicalTurnCount += turnDerection ? 1 : -1;
        if (_identicalTurnCount == 3 || _identicalTurnCount == -3)
        {
            turnDerection = !turnDerection;
            _identicalTurnCount = 0;
        }
        _currentRotate += new Vector2Int(0, 90 * (turnDerection ? -1 : 1));
        _currentDirection = turnDerection ?
            new Vector2Int( _currentDirection.y, - _currentDirection.x):
            new Vector2Int(-_currentDirection.y, _currentDirection.x);
        
    }

    private void CompleateGenerate()
    {
        while(_currentPoss.x != 0)
        {
            _currentPoss.x -= (_currentPoss.x > 0) ? 1 : -1;
            Instantiate(Road, (Vector3Int)_currentPoss, Quaternion.Euler(0, 0, _currentRotate.y), transform);
            if (Math.Abs(_currentPoss.x) < 1)
                 _currentPoss.x = 0;
        }
        while(_currentPoss.y != 0)
        {
            _currentPoss.y -= (_currentPoss.y > 0) ? 1 : -1;
            Instantiate(Road, (Vector3Int)_currentPoss, Quaternion.Euler(0, 0, _currentRotate.y), transform);
            if (Math.Abs(_currentPoss.y) < 1)
                _currentPoss.y = 0;
        }
    }

    private Vector2 GetCenter()
    {
        return (Vector2)_PointOnMaxDistance * 0.5f;
    }
}
