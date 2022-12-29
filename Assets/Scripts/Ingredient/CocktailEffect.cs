using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CocktailEffect
{
    public CocktailEffect(CocktailEffectType type)
    {
        CocktailEffectFactory.Register(type, this);
    }
    public abstract void Affect(CocktailController cocktail);
}

public enum CocktailEffectType
{
    GROW, GLOW
}
