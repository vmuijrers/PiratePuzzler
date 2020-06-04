using UnityEngine;

public class GameStateNotifier : MonoBehaviour
{
    public void OnWinGame()
    {
        GameController.Instance.WinGame();
    }
    public void OnLoseGame()
    {
        GameController.Instance.LoseGame();
    }
}
