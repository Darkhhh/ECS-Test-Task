using UnityEngine;

public class RestartButtonHandler : MonoBehaviour
{
    public void RestartGame()
    {
        var restartEntity = Contexts.sharedInstance.game.CreateEntity();
        restartEntity.isRestartGame = true;
    }
}
