using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

public class ActivateIngotSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    
    public ActivateIngotSystem(Contexts contexts) : base(contexts.game)
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
        var ingots = _gameContext.GetGroup(GameMatcher.Ingot).GetEntities();
        foreach (var tile in entities)
        {
            if (tile.depth.value == tile.ingotDepth.value)
            {
                foreach (var ingot in ingots)
                {
                    if (ingot.position.value == tile.position.value)
                    {
                        ingot.view.gameObject.SetActive(true);
                        tile.isActiveIngot = true;
                        break;
                    }
                }
            }
        }
    }
}