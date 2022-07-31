using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class CreateIngotsSystem : IInitializeSystem, ICleanupSystem
{
    private readonly Contexts _context;
    private readonly Sprite _sprite;
    private readonly Transform _parent;
    private readonly List<Vector3> _positions;
    private readonly Color _goldColor = Color.yellow;

    public CreateIngotsSystem(Contexts contexts, Sprite sprite, Transform parent, List<Vector3> positions)
    {
        _context = contexts;
        _sprite = sprite;
        _parent = parent;
        _positions = positions;
    }

    public void Initialize()
    {
        foreach (var position in _positions)
        {
            var entity = _context.game.CreateEntity();
            entity.AddPosition(position);
            entity.isIngot = true;
            
            var gameObject = new GameObject("IngotClone");
            gameObject.transform.SetParent(_parent,false);
            var renderer = gameObject.AddComponent<SpriteRenderer>();
            renderer.sprite = _sprite;
            renderer.color = _goldColor;
            renderer.sortingOrder = 1;
            gameObject.transform.position = entity.position.value;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 45);
            gameObject.transform.localScale = new Vector3(0.75f, 0.3f, 1);
        
            entity.AddView(gameObject);
            gameObject.Link(entity);
            entity.view.gameObject.SetActive(false);
        }
    }
    
    public void Cleanup()
    {
        var entities = _context.game.GetGroup(GameMatcher.Ingot).GetEntities();
        foreach (var entity in entities)
        {
            entity.view.gameObject.Unlink();
            entity.RemoveView();
        }
    }
}