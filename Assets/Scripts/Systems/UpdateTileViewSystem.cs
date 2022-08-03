using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class UpdateTileViewSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    
    public UpdateTileViewSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Opacity);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isClicked && entity.isTile && !entity.isActiveIngot && entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var spriteRenderer = e.view.gameObject.GetComponent<SpriteRenderer>();
            var color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, e.opacity.value);
        }
    }
}