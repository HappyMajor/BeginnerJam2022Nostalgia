using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Ingredient.Effects
{
    class GrowEffect : CocktailEffect
    {

        public GrowEffect() : base(CocktailEffectType.GROW) { }

        public override void Affect(CocktailController cocktail)
        {
            cocktail.transform.localScale = new UnityEngine.Vector3(2f, 2f, 2f);
        }
    }

}
