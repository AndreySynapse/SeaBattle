using System.Collections.Generic;
using UnityEngine;

public class Placeholder
{
    private struct ListsLine
    {
        public int index;
        public List<List<int>> lists;
    }

    private int _width;
    private int _height;
    private FillTypes[,] _field;

    public Placeholder()
    {

    }

    public Placeholder(FillTypes[,] field, int width, int height) // Obsolete
    {
        _field = field;
        _width = width;
        _height = height;
    }
    
    public FleetPlacement GeneratePlacement(Field field, ShipsInventory inventory)
    {
        _field = field.FieldFilling;
        _width = field.Size.x;
        _height = field.Size.y;

        FleetPlacement fleet = new FleetPlacement();

        Fill(fleet, inventory);

        return fleet;
    }

    public void Fill(FleetPlacement fleet, ShipsInventory inventory)
    {
        foreach (ShipsInventory.ShipsCell item in inventory.Ships)
        {
            for (int i = 0; i < item.count; i++)
            {
                ShipOrientations direction = Random.Range(0, 2) == 0 ? ShipOrientations.Horizontal : ShipOrientations.Vertical;
                int length = item.ship.Length;

                var lines = direction == ShipOrientations.Horizontal ? GetFreeHorizontalCells(length) : GetFreeVerticalCells(length);
                int basePos = GetRandomIndexFromLineList(lines);

                int listIndex = Random.Range(0, lines[basePos].lists.Count);
                var list = lines[basePos].lists[listIndex];

                int secondPos = list[Random.Range(0, (list.Count - length) + 1)];

                FleetPlacement.ShipPlacement data = new FleetPlacement.ShipPlacement();
                data.Orientation = direction;
                data.Position = direction == ShipOrientations.Horizontal ? new Vector2Int(secondPos, basePos) : new Vector2Int(basePos, secondPos);
                data.ShipData = item.ship;

                fleet.Placements.Add(data);

                FillShipPosition(direction, length, direction == ShipOrientations.Horizontal ? secondPos : basePos, direction == ShipOrientations.Horizontal ? basePos : secondPos);
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

    private int GetRandomIndexFromLineList(List<ListsLine> list)
    {
        List<int> filledList = new List<int>();

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].lists != null && list[i].lists.Count > 0)
                filledList.Add(i);
        }

        return filledList[Random.Range(0, filledList.Count)];
    }

    private void FillShipPosition(ShipOrientations orientation, int length, int x, int y)
    {
        int minLimitX = Mathf.Clamp(x - 1, 0, _width - 1);
        int maxLimitX = orientation == ShipOrientations.Horizontal ? Mathf.Clamp(x + length, 0, _width - 1) : Mathf.Clamp(x + 1, 0, _width - 1);

        int minLimitY = Mathf.Clamp(y - 1, 0, _height - 1);
        int maxLimitY = orientation == ShipOrientations.Horizontal ? Mathf.Clamp(y + 1, 0, _height - 1) : Mathf.Clamp(y + length, 0, _height - 1);

        for (int i = minLimitX; i <= maxLimitX; i++)
            for (int j = minLimitY; j <= maxLimitY; j++)
            {
                _field[i, j] = FillTypes.Ship;
            }
    }
}
