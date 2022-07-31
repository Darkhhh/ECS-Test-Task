using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

public class DigTileSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    
    public DigTileSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Clicked);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isClicked && entity.isTile && !entity.isActiveIngot;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var modelEntity = _gameContext.GetGroup(GameMatcher.Model).GetEntities().FirstOrDefault();
        if (modelEntity is null) throw new Exception("Can not access ModelEntity");
        foreach (var e in entities)
        {
            if (e.depth.value >= 1)
            {
                e.depth.value--;
                var shovels = modelEntity.shovels.value - 1;
                modelEntity.ReplaceShovels(shovels);
            }
        }
    }
}