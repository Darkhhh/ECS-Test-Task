//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public DepthComponent depth { get { return (DepthComponent)GetComponent(GameComponentsLookup.Depth); } }
    public bool hasDepth { get { return HasComponent(GameComponentsLookup.Depth); } }

    public void AddDepth(int newValue) {
        var index = GameComponentsLookup.Depth;
        var component = (DepthComponent)CreateComponent(index, typeof(DepthComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDepth(int newValue) {
        var index = GameComponentsLookup.Depth;
        var component = (DepthComponent)CreateComponent(index, typeof(DepthComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDepth() {
        RemoveComponent(GameComponentsLookup.Depth);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherDepth;

    public static Entitas.IMatcher<GameEntity> Depth {
        get {
            if (_matcherDepth == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Depth);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDepth = matcher;
            }

            return _matcherDepth;
        }
    }
}
