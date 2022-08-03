using System.Collections.Generic;
using Entitas;

public class DeactivateIngotSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    
    public DeactivateIngotSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Clicked);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isClicked && entity.isIngot;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var ingot in entities)
        {
            ingot.view.gameObject.SetActive(false);
        }
    }
}