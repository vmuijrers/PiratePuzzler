using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level : MonoBehaviour
{
    public Dictionary<Vector3Int, Tile> grid = new Dictionary<Vector3Int, Tile>();
    public List<Unit> Units { get; private set; }
    public List<Tile> RemovedTiles = new List<Tile>();
    public void Init()
    {
        if(Units != null && Units.Count > 0)
        {
            Units.ToList().ForEach(x => Destroy(x.gameObject));
            Units.Clear();
        }

        grid.Clear();
        grid = new Dictionary<Vector3Int, Tile>();
        Tile[] tiles = FindObjectsOfType<Tile>();
        foreach(Tile t in tiles)
        {
            Vector3Int gridPosition = Vector3Int.RoundToInt(t.transform.position);
            grid.Add(gridPosition, t);
            t.SetPosition(gridPosition);
            t.Init();
        }
        foreach(Tile t in RemovedTiles)
        {
            Vector3Int gridPosition = Vector3Int.RoundToInt(t.transform.position);
            grid.Add(gridPosition, t);
            t.SetPosition(gridPosition);
            t.Init();
        }
        RemovedTiles.Clear();
        Units = FindObjectsOfType<Unit>().ToList();
        for (int i = 0; i < Units.Count; i++)
        {
            Units[i].Init();
        }
    }

    public Tile GetTileForPosition(Vector3 position)
    {
        if (grid.ContainsKey(Vector3Int.RoundToInt(position)))
        {
            return grid[Vector3Int.RoundToInt(position)];
        }
        return null;
    }

    public Tile FindTileForUnit(Unit unit)
    {
        return grid.Values.ToList().Find(x => !x.IsFree() && x.Unit.gameObject == unit.gameObject);
    }

    public Tile GetPlayerTile()
    {
        return GameController.Instance.Player.CurrentTile;
    }
}
