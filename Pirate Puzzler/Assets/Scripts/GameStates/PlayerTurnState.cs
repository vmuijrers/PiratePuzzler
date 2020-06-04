using UnityEngine;

public class PlayerTurnState : State
{
    public LayerMask tileLayer;
    public override void OnEnter()
    {
        Debug.Log("Entered Player Turn State");
    }

    public override void OnExit()
    {
        Debug.Log("Exited Player Turn State");
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        //Try Moving
        if (Input.GetMouseButton(0))
        {
            Tile hitTile = GetTileFromRaycast();
            if (hitTile != null)
            {
                Tile playerTile = GameController.Instance.Level.GetPlayerTile();
                float distance = Vector3Int.Distance(hitTile.Position, playerTile.Position);
                if (Mathf.Approximately(distance, 1f)) 
                { 
                    if (hitTile.IsFree())
                    {
                        GameController.Instance.Player.LookAtPosition(hitTile.Position);
                        GameController.Instance.Player.MoveToTile(hitTile);
                        GameController.Instance.EndPlayerTurn();
                    }
                }
            }

        }
         
        //Try to dig
        if (Input.GetMouseButton(1))
        {
            Tile hitTile = GetTileFromRaycast();
            if (hitTile != null)
            {
                Tile playerTile = GameController.Instance.Level.GetPlayerTile();
                if(Vector3Int.Distance(hitTile.Position, playerTile.Position) == 1)
                {
                    GameController.Instance.Player.LookAtPosition(hitTile.Position);
                    GameController.Instance.Player.Attack();
                    if (hitTile.IsFree())
                    {
                        hitTile.Dig();
                    }
                    else
                    {
                        hitTile.Unit.GetStunned();
                        hitTile.Unit.MoveInDirection(hitTile.Position - playerTile.Position, true);
                    }
                    GameController.Instance.EndPlayerTurn();
                }
            }
            
        }
    }

    public Tile GetTileFromRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, tileLayer))
        {
            Tile tile = hit.collider.GetComponentInParent<Tile>();
            if (tile != null)
            {
                return tile;
            }
        }
        return null;
    }
}
