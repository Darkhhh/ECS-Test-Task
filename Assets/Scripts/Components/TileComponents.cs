using Entitas;
using UnityEngine;

[Game]
public class TileComponent : IComponent
{
    
}

[Game]
public class DepthComponent : IComponent
{
    public int value;
}

[Game]
public class IngotDepthComponent : IComponent
{
    public int value;
}

[Game]
public class OpacityComponent : IComponent
{
    public float value;
}

[Game]
public class ActiveIngotComponent : IComponent
{
    
}

[Game]
public class SpriteRendererComponent : IComponent
{
    public SpriteRenderer value;
}