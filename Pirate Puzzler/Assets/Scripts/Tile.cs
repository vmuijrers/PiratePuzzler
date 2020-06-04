using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public Unit Unit { get; private set; }
    public Vector3Int Position { get; private set; }

    public UnityEvent OnInitializedEvent;
    public UnityEvent OnDiggedEvent;
    private Material mat;
    private Color baseColor;
    
    private void Awake()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
        baseColor = mat.color;
    }

    public void Init()
    {
        gameObject.SetActive(true);
        Unit = null;
        OnInitializedEvent?.Invoke();
    }
    public void OnMouseEnter()
    {
        mat.color = Color.cyan;
    }

    public void OnMouseExit()
    {
        mat.color = baseColor;
    }

    public void SetPosition(Vector3Int pos)
    {
        Position = pos;
        transform.position = pos;
    }

    public bool IsFree()
    {
        return Unit == null;
    }

    public void Dig()
    {
        if(Unit != null) { return; }
        OnDiggedEvent?.Invoke();
        GameController.Instance.Level.grid.Remove(Position);
        GameController.Instance.Level.RemovedTiles.Add(this);
        gameObject.SetActive(false);
    }

    public void AssignUnit(Unit unit)
    {
        this.Unit = unit;
    }

    public void ReleaseUnit()
    {
        Unit = null;
    }

    public Tile[] GetNeighbours()
    {
        return GameController.Instance.Level.grid.Values.ToList().FindAll(x => Vector3Int.Distance(Position, x.Position) == 1).ToArray();
    }
}
