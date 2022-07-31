using System.Collections.Generic;
using Entitas;

public class RemoveClickComponentSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    
    public RemoveClickComponentSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Clicked);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isClicked;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.isClicked = false;
        }
    }
}