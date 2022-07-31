using System;
using Entitas;
using UnityEngine;

public class MouseInputSystem : IExecuteSystem
{
    private readonly Contexts _context;
    private readonly Camera _gameCamera;
    private const float TileEdgeLength = 0.5f;
    private const float IngotLength = 0.75f;
    private const float IngotWidth = 0.3f;

    public MouseInputSystem(Contexts contexts, Camera gameCamera)
    {
        _context = contexts;
        _gameCamera = gameCamera;
    }

    public void Execute()
    {
        if (Input.GetKeyDown (KeyCode.Mouse0))
        {
            var clickPosition = _gameCamera.ScreenToWorldPoint(Input.mousePosition);
            var tiles = _context.game.GetGroup(GameMatcher.Tile).GetEntities();
            foreach (var tile in tiles)
            {
                if(tile.isActiveIngot) continue;
                var minX = tile.position.value.x - TileEdgeLength;
                var maxX = tile.position.value.x + TileEdgeLength;
                var minY = tile.position.value.y - TileEdgeLength;
                var maxY = tile.position.value.y + TileEdgeLength;
                var x = clickPosition.x;
                var y = clickPosition.y;
                if (minX < x && x < maxX && minY < y && y < maxY)
                {
                    tile.isClicked = true;
                    break;
                }
            }

            var ingots = _context.game.GetGroup(GameMatcher.Ingot);
            foreach (var ingot in ingots)
            {
                if (ingot.view.gameObject.activeSelf)
                {
                    var x = ingot.position.value.x - clickPosition.x;
                    var y = ingot.position.value.y - clickPosition.y;
                    if (-(IngotWidth / Math.Sqrt(2)) < y - x && y - x < IngotWidth / Math.Sqrt(2) &&
                        -(IngotLength / (2 * Math.Sqrt(2))) < y + x && y + x < IngotLength / (2 * Math.Sqrt(2)))
                    {
                        ingot.isClicked = true;
                        break;
                    }
                }
            }
        }
    }
}