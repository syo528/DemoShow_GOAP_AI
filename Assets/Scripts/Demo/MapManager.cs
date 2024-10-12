using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public Stack<Vector2Int> backupCells = new Stack<Vector2Int>(); // 放置物品的备用格子
    public int currentCircleNum = 2;    // 圈数
    public float cellSize = 2f;
    private void Awake()
    {
        Instance = this;
    }

    private void CreateBackupCells()
    {
        currentCircleNum += 1;
        int minX = -currentCircleNum;
        int minY = -currentCircleNum;
        int sideLenght = currentCircleNum * 2 + 1; // 一侧的长度
        // 遍历左边 x固定最小值
        for (int y = minY; y < minY + sideLenght; y++)
        {
            backupCells.Push(new Vector2Int(minX, y));
        }
        // 遍历上边 y是固定最大值
        int maxY = minY + sideLenght - 1;
        for (int x = minX + 1; x < minX + sideLenght; x++)
        {
            backupCells.Push(new Vector2Int(x, maxY));
        }
        // 遍历右边 x是固定最大值
        int maxX = minX + sideLenght - 1;
        for (int y = maxY - 1; y >= minY; y--)
        {
            backupCells.Push(new Vector2Int(maxX, y));
        }
        // 遍历下边 y是固定的最小值
        for (int x = maxX - 1; x > minX; x--)
        {
            backupCells.Push(new Vector2Int(x, minY));
        }
    }

    public Vector3 GetCellPosition(Vector2Int coord)
    {
        return new Vector3(coord.x * cellSize, 0, coord.y * cellSize);
    }
    public Vector2Int GetNextBuildCoord()
    {
        if (backupCells.Count == 0)
        {
            CreateBackupCells();
        }
        return backupCells.Pop();
    }
}
