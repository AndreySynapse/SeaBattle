using System.Collections.Generic;

public class PlayerBattleField : BattleField
{
    public struct SpaceLine
    {
        public int index;
        public List<int> list;
    }

    protected override void Init()
    {
        base.Init();
        
        _shot.SetAsLastSibling();
    }

    public List<SpaceLine> GetFreeHorizontalSpace()
    {
        List<SpaceLine> list = new List<SpaceLine>();
        int width = _logicSize.x;
        int height = _logicSize.y;

        for (int y = 0; y < height; y++)
        {
            SpaceLine line = new SpaceLine();
            line.index = y;
            line.list = new List<int>();

            for (int x = 0; x < width; x++)
                if (this.FieldFilling[x, y] != FillTypes.WreckedShip && this.FieldFilling[x, y] != FillTypes.Shot)
                    line.list.Add(x);

            if (line.list.Count > 0)
                list.Add(line);
        }

        return list;
    }
}