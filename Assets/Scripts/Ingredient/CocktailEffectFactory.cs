using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class CocktailEffectFactory
{
    private static Dictionary<CocktailEffectType, CocktailEffect> instances = new Dictionary<CocktailEffectType, CocktailEffect>();

    public static void Register(CocktailEffectType type, CocktailEffect effect)
    {
        instances[type] = effect;
    }

}

