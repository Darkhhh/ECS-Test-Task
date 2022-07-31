using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

public class RemoveIngotSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    
    public RemoveIngotSystem(Contexts contexts) : base(contexts.game)
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
        var modelEntity = _gameContext.GetGroup(GameMatcher.Model).GetEntities().FirstOrDefault();
        if (modelEntity is null) throw new Exception("Can not access ModelEntity");
        var tiles = _gameContext.GetGroup(GameMatcher.Tile).GetEntities();
        foreach (var ingot in entities)
        {
            ingot.view.gameObject.SetActive(false);
            var collectedIngots = modelEntity.collectedIngots.value + 1;
            modelEntity.ReplaceCollectedIngots(collectedIngots);
            foreach (var tile in tiles)
            {
                if (ingot.position.value == tile.position.value)
                {
                    tile.isActiveIngot = false;
                    break;
                }
            }
        }
    }
}