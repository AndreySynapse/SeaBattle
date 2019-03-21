using System.Collections.Generic;
using UnityEngine;

public class FieldPlaceholder
{
    private struct ListsLine
    {
        public int index;
        public List<List<int>> lists;
    }

    private int _width;
    private int _height;
    private FillTypes[,] _field;

    public FieldPlaceholder(FillTypes[,] field, int width, int height)
    {
        _field = field;
        _width = width;
        _height = height;
    }

    public void Fill(FleetPlacement fleet, ShipsInventory inventory)
    {
        foreach (ShipsInventory.ShipsCell item in inventory.Ships)
        {
            for (int i = 0; i < item.count; i++)
            {
                ShipOrientations orientation = Random.Range(0, 2) == 0 ? ShipOrientations.Horizontal : ShipOrientations.Vertical;
                orientation = ShipOrientations.Horizontal;
                int length = item.ship.Length;

                var lines = orientation == ShipOrientations.Horizontal ? GetFreeHorizontalCells(length) : GetFreeVerticalCells(length);
                int basePosition = GetRandomIndex(lines);

                int listIndex = Random.Range(0, lines[basePosition].lists.Count);
                var list = lines[basePosition].lists[listIndex];

                int additionalPosition = list[Random.Range(0, (list.Count - length) + 1)];

                FleetPlacement.ShipPlacement data = new FleetPlacement.ShipPlacement();
                data.Orientation = orientation;
                data.Position = orientation == ShipOrientations.Horizontal ? new Vector2Int(additionalPosition, basePosition) : new Vector2Int(basePosition, additionalPosition);
                data.ShipData = item.ship;

                fleet.Placements.Add(data);

                if (orientation == ShipOrientations.Horizontal)
                    FillHorizontalShipPosition(item.ship.Length, additionalPosition, basePosition);
                else
                    FillVerticalShipPosition(item.ship.Length, basePosition, additionalPosition);
            }
        }
    }

    private List<ListsLine> GetFreeHorizontalCells(int length)
    {
        List<ListsLine> list = new List<ListsLine>();

        for (int y = 0; y < _height; y++)
        {
            ListsLine line = new ListsLine();
            line.index = y;
            line.lists = new List<List<int>>();

            int count = 0;
            for (int x = 0; x < _width; x++)
            {
                if (count >= length && (_field[x, y] != FillTypes.Empty || x == _width - 1))
                {
                    List<int> l = new List<int>();
                    int limit = (x == _width - 1 && _field[x, y] == FillTypes.Empty) ? x + 1 : x;
                    for (int i = x - count; i < limit; i++)
                        l.Add(i);
                                        
                    line.lists.Add(l);
                    count = 0;
                }
                else
                    count = _field[x, y] == FillTypes.Empty ? count + 1 : 0;
            }

            list.Add(line);
        }

        return list;
    }

    private List<ListsLine> GetFreeVerticalCells(int length)
    {
        List<ListsLine> list = new List<ListsLine>();

        for (int x = 0; x < _width; x++)
        {
            ListsLine line = new ListsLine();
            line.index = x;
            line.lists = new List<List<int>>();

            int count = 0;
            for (int y = 0; y < _height; y++)
            {
                if (count >= length && (_field[x, y] != FillTypes.Empty || y == _height - 1))
                {
                    List<int> l = new List<int>();
                    int limit = (y == _height - 1 && _field[x, y] == FillTypes.Empty) ? y + 1 : y;
                    for (int i = y - count; i < limit; i++)
                        l.Add(i);

                    line.lists.Add(l);
                    count = 0;
                }
                else
                    count = _field[x, y] == FillTypes.Empty ? count + 1 : 0;
            }

            list.Add(line);
        }

        return list;
    }

    private int GetRandomIndex(List<ListsLine> list)
    {
        List<int> filledList = new List<int>();

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].lists != null && list[i].lists.Count > 0)
                filledList.Add(i);
        }

        return filledList[Random.Range(0, filledList.Count)];
    }

    private void FillHorizontalShipPosition(int length, int x, int y)
    {
        int minLimitX = Mathf.Clamp(x - 1, 0, _width - 1);
        int maxLimitX = Mathf.Clamp(x + length, 0, _width - 1);

        int minLimitY = Mathf.Clamp(y - 1, 0, _height - 1);
        int maxLimitY = Mathf.Clamp(y + 1, 0, _height - 1);

        for (int i = minLimitX; i <= maxLimitX; i++)
            for (int j = minLimitY; j <= maxLimitY; j++)
            {
                _field[i, j] = FillTypes.Ship;
            }
    }

    private void FillVerticalShipPosition(int length, int x, int y)
    {
        int minLimitX = Mathf.Clamp(x - 1, 0, _width - 1);
        int maxLimitX = Mathf.Clamp(x + 1, 0, _width - 1);

        int minLimitY = Mathf.Clamp(y - 1, 0, _height - 1);
        int maxLimitY = Mathf.Clamp(y + length, 0, _height - 1);

        for (int i = minLimitX; i <= maxLimitX; i++)
            for (int j = minLimitY; j <= maxLimitY; j++)
            {
                _field[i, j] = FillTypes.Ship;
            }
    }
}
