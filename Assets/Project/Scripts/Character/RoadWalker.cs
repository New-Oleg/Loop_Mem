using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadWalker : MonoBehaviour
{
    public static event Action DropCard;


    [Tooltip("“ег тайлов дороги (по умолчанию 'Tail')")]
    public string roadTag = "Tail";

    [Tooltip("—корость движени€")]
    public float moveSpeed = 3f;

    private HashSet<Vector2Int> roadTiles = new HashSet<Vector2Int>();
    private List<Vector2Int> path = new List<Vector2Int>();

    private Vector2Int startGrid;
    private Vector2Int currentDir;
    private int pathIndex = 0;
    private bool IsGo = true;

    private void OnEnable()
    {
        ACard.CardSelect += Go;

        Tail.StartFight += Stap;
        FightController.EndFight += Go;

        ShopController.GenerateNewLevel += Start;
    }

    private void OnDisable()
    {
        ACard.CardSelect -= Go;

        Tail.StartFight -= Stap;
        FightController.EndFight -= Go;

        ShopController.GenerateNewLevel -= Start;
    }

    private void Start()
    {
        GoToZero();

        Invoke(nameof(CollectRoadTiles), 0.15f);

        // строим маршрут один раз
        Invoke(nameof(BuildPath), 0.15f);
        // запускаем движение


    }
    

    private void CollectRoadTiles()
    {
        roadTiles.Clear();
        // ѕеребираем детей общего родител€ Ч это гарантирует локальные позиции и независимость от пор€дка в иерархии.
        Transform parent = transform.parent;

        foreach (Transform child in parent)
        {
            if (child == transform) continue; // пропускаем сам персонаж

            Vector2 local = new Vector2(child.localPosition.x, child.localPosition.y);
            Vector2Int key = new Vector2Int(Mathf.RoundToInt(local.x), Mathf.RoundToInt(local.y));
            if (!roadTiles.Contains(key))
                roadTiles.Add(key);
        }
    }

    private Vector2Int FindInitialDirection()
    {
        Vector2Int[] dirs = {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left };

        foreach (var d in dirs)
        {
            if (roadTiles.Contains(startGrid + d))
                return d;
        }
        return Vector2Int.zero;
    }


    private void BuildPath()
    {
        currentDir = FindInitialDirection();

        path.Clear();

        Vector2Int currentPos = startGrid;
        Vector2Int dir = currentDir;

        HashSet<string> visitedStates = new HashSet<string>();
        int maxSteps = roadTiles.Count * 4; // защита от зацикливани€

        int steps = 0;
        while (steps < maxSteps)
        {
            steps++;

            // —охран€ем состо€ние (позици€ + направление)
            string state = currentPos.x + "," + currentPos.y + ":" + dir.x + "," + dir.y;
            if (visitedStates.Contains(state) && path.Count > 0)
            {
                Debug.LogWarning("[RoadWalker] ќбнаружено зацикливание на состо€нии " + state);
                break;
            }
            visitedStates.Add(state);

            Vector2Int nextDir = ChooseNextDirection(currentPos, dir);
            Vector2Int nextPos = currentPos + nextDir;

            if (!roadTiles.Contains(nextPos))
            {
                Debug.LogWarning($"[RoadWalker] Ќет дороги в {nextPos}, построение прервано.");
                break;
            }

            path.Add(nextPos);

            // ≈сли вернулись на стартовую клетку Ч замыкаем путь
            if (nextPos == startGrid && path.Count > 1)
                break;

            currentPos = nextPos;
            dir = nextDir;
        }

        if(path.Count < (transform.parent.childCount - 1) / 2)
        {
            Debug.LogWarning($"[RoadWalker] ѕуть слишком короткий ({path.Count}/{roadTiles.Count}). ѕопробуем изменить стартовую клетку.");

            // Ќаходим кандидатов (все тайлы, которых нет в пути)
            var candidates = roadTiles.Except(path).ToList();

            if (candidates.Count > 0)
            {
                // ћожно вз€ть первую
                startGrid = candidates[0];

                // или случайную:
                // startGrid = candidates[Random.Range(0, candidates.Count)];

                currentDir = FindInitialDirection();
                BuildPath();
                return;
            }
        }

        DestroyUnusedTiles();
        StartCoroutine(WalkPath());
    }

    private void DestroyUnusedTiles()
    {
        if (transform.parent == null) return;

        HashSet<Vector2Int> pathSet = new HashSet<Vector2Int>(path);


        foreach (Transform child in transform.parent)
        {
            if (!child.CompareTag(roadTag)) continue;

            Vector2Int cell = new Vector2Int(
                Mathf.RoundToInt(child.localPosition.x),
                Mathf.RoundToInt(child.localPosition.y)
            );

            if (!pathSet.Contains(cell) && cell != startGrid)
            {
                //Destroy(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
    }

    private Vector2Int ChooseNextDirection(Vector2Int pos, Vector2Int dir)
    {
        Vector2Int right = new Vector2Int(dir.y, -dir.x);
        Vector2Int straight = dir;
        Vector2Int left = new Vector2Int(-dir.y, dir.x);
        Vector2Int back = -dir;

        if (roadTiles.Contains(pos + right)) return right;
        if (roadTiles.Contains(pos + straight)) return straight;
        if (roadTiles.Contains(pos + left)) return left;
        if (roadTiles.Contains(pos + back)) return back;

        return back;
    }

    private IEnumerator WalkPath()
    {
        while (true)
        {
            if (IsGo == true)
            {
                Vector2Int targetCell = path[pathIndex];
                Vector3 target = new Vector3(targetCell.x, targetCell.y, 0f);
                while ((transform.localPosition != target))
                {
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition,
                        target, moveSpeed * Time.deltaTime);
                    yield return null;
                }

                pathIndex++;
                if (transform.localPosition == Vector3.zero && pathIndex >1)
                {
                    Stap();
                    DropCard.Invoke();
                    pathIndex = 0; // зациклить путь
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    private void Stap()
    {
        IsGo = false;
    }
    private void Go()
    {
        IsGo = true;
    }

    private void GoToZero()
    {
        pathIndex = 0;
        StopAllCoroutines();
        transform.localPosition = Vector3.zero;
    }
}