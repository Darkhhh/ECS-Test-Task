using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public class ClickedComponent : IComponent
{
    
}

[Game]
public class ShovelsComponent : IComponent
{
    public int value;
}

[Game]
public class CollectedIngotsComponent : IComponent
{
    public int value;
}

[Game]
public class DefaultDepthComponent : IComponent
{
    public int value;
}

[Game]
public class WinGoldNumberComponent : IComponent
{
    public int value;
}

[Game]
public class DefaultShovelsNumberComponent : IComponent
{
    public int value;
}

[Game, Unique]
public class RestartGameComponent : IComponent
{
    
}

[Game, Unique]
public class ModelComponent : IComponent
{
    
}
