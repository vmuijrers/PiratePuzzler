using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public Tile CurrentTile { get; protected set; }
    public bool IsDead { get; protected set; }
    public bool IsStunned { get; protected set; }

    public virtual void Die()
    {
        IsDead = true;
    }

    public void Init()
    {
        Level grid = GameController.Instance.Level;
        Tile tile = grid.GetTileForPosition(transform.position);
        if(tile != null && tile.Unit == null)
        {
            Debug.Log("Assigned Unit: " + name + " To tile: " + tile.name);
            tile.AssignUnit(this);
            CurrentTile = tile;
        }
    }

    public void MoveToTile(Tile tile)
    {
        if (tile.IsFree())
        {
            if (CurrentTile != null)
            {
                CurrentTile.ReleaseUnit();
            }
            tile.AssignUnit(this);
            CurrentTile = tile;
            LookAtPosition(tile.Position);
            StartCoroutine(MoveToPosition(tile.Position));
        }
    }

    public virtual void GetStunned()
    {
        IsStunned = true;
    }

    public void MoveInDirection(Vector3Int direction, bool isPushed = false)
    {
        Level grid = GameController.Instance.Level;
        Tile tile = grid.GetTileForPosition(transform.position + direction);
        if(tile != null)
        {
            if (tile.IsFree())
            {
                if (CurrentTile != null)
                {
                    CurrentTile.ReleaseUnit();
                }
                tile.AssignUnit(this);
                CurrentTile = tile;
                if (isPushed)
                {
                    LookAtPosition(GameController.Instance.Player.CurrentTile.Position);
                }
                StartCoroutine(MoveToPosition(tile.Position));
            }
        }
        else
        {
            if (CurrentTile != null)
            {
                CurrentTile.ReleaseUnit();
            }
            LookAtPosition(CurrentTile.Position - direction);
            StartCoroutine(MoveToPosition(CurrentTile.Position + direction, Die));
            CurrentTile = null;
        }
    }

    protected virtual IEnumerator MoveToPosition(Vector3Int targetPosition, System.Action onDone = null)
    {
        Vector3 startPosition = transform.position;
        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        transform.position = targetPosition;
        onDone?.Invoke();
    }

    public void LookAtPosition(Vector3Int position)
    {
        transform.rotation = Quaternion.LookRotation((position - transform.position).normalized, Vector3.up);
    }
}
