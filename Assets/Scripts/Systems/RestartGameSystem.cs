using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameSystem : ReactiveSystem<GameEntity>
{
    GameContext _context;

    public RestartGameSystem(Contexts contexts) : base(contexts.game) {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.RestartGame.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity) {
        return entity.isRestartGame;
    }

    protected override void Execute(List<GameEntity> entities) {
        if (!(entities is null) && entities.Count > 0)
        {
            var allEntities = _context.GetEntities();
            foreach (var entity in allEntities)
            {
                if (entity.hasView)
                {
                    entity.view.gameObject.Unlink();
                }
                entity.Destroy();
            }
            
            Debug.Log("Scene Restarted");
            SceneManager.LoadScene("SampleScene");
        }
    }
}
