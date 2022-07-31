using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class ChangeOpacitySystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    
    public ChangeOpacitySystem(Contexts contexts) : base(contexts.game)
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
            e.opacity.value = e.depth.value / (float) modelEntity.defaultDepth.value;
            var spriteRenderer = e.view.gameObject.GetComponent<SpriteRenderer>();
            var color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, e.opacity.value);
        }
    }
}