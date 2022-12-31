using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class IngredientController : MonoBehaviour
{
    public IngredientSO ingredientSO;

    private bool selected = false;
    private CocktailController collidingWithCocktail = null;
    private QuaternionTween rotateNormal;
    private QuaternionTween rotateLeft;
    private Coroutine pourRoutine;
    public ParticleSystem pourEffect;

    private Vector3 startPosition;

    public void Select()
    {
        this.selected = true;
    }

    public void DeSelect()
    {
        this.selected = false;
        if(collidingWithCocktail)
        {
            if (ingredientSO.type == IngredientSO.IngredientType.SPICE)
            {
                PourIntoCocktail(collidingWithCocktail);
            } else if(ingredientSO.type == IngredientSO.IngredientType.BASE)
            {
                TweenFactory.Tween("jumpBack", transform.position, startPosition, 0.5f, TweenScaleFunctions.QuadraticEaseOut, (t) => transform.position = t.CurrentValue, null);
                StopPouringBaseIntoCocktail();
            }
        } else
        {
            TweenFactory.Tween("jumpBack", transform.position, startPosition, 0.5f, TweenScaleFunctions.QuadraticEaseOut, (t) => transform.position = t.CurrentValue, null);
        }
    }

    public void JumpBackToSlot()
    {

    }

    public void PourIntoCocktail(CocktailController cocktail)
    {
        if(ingredientSO.type == IngredientSO.IngredientType.SPICE)
        {
            cocktail.AddBase(this,33);
            gameObject.SetActive(false);
        }
    }

    public void PourBaseIntoCocktail(CocktailController cocktail)
    {
        if(pourRoutine == null)
        {
            TweenFactory.RemoveTweenKey("rotateNormal", TweenStopBehavior.Complete);
            RotateBottleLeft();
            pourRoutine = StartCoroutine(PourBase(cocktail));
            ParticleSystem.MainModule main = pourEffect.main;
            main.startColor = ingredientSO.baseColor;
            pourEffect.Play();
        }
    }

    public IEnumerator PourBase(CocktailController cocktail)
    {
        while(true)
        {
            cocktail.AddBase(this, 1);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void StopPouringBaseIntoCocktail()
    {
        TweenFactory.RemoveTweenKey("rotateLeft", TweenStopBehavior.Complete);
        RotateBottleNormal();
        pourEffect.Stop();
        if (pourRoutine != null)
        {
            StopCoroutine(pourRoutine);
            pourRoutine = null;
        }
    }

    public void RotateBottleLeft()
    {
        if(rotateLeft == null || rotateLeft.State == TweenState.Stopped)
        {
            rotateLeft = TweenFactory.Tween("rotateLeft", transform.rotation, Quaternion.Euler(new Vector3(0, 0, 90)), 0.25f, TweenScaleFunctions.CubicEaseIn, (t) => transform.rotation = t.CurrentValue, null);
        }
    }

    public void RotateBottleNormal()
    {
        if (rotateNormal == null || rotateNormal.State == TweenState.Stopped)
        {
            rotateNormal = TweenFactory.Tween("rotateNormal", transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), 0.25f, TweenScaleFunctions.CubicEaseIn, (t) => transform.rotation = t.CurrentValue, null);
        }
    }

    private void OnMouseDown()
    {
        Select();
    }

    private void OnMouseUp()
    {
        DeSelect();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CocktailController>() != null)
        {
            collidingWithCocktail = collision.gameObject.GetComponent<CocktailController>();

            if (ingredientSO.type == IngredientSO.IngredientType.BASE)
            {
                PourBaseIntoCocktail(collidingWithCocktail);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == collidingWithCocktail.gameObject)
        {
            collidingWithCocktail = null;

            if (ingredientSO.type == IngredientSO.IngredientType.BASE)
            {
                StopPouringBaseIntoCocktail();
            }
       
        }
    }

    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        GetComponent<SpriteRenderer>().size = new Vector2(1.5f, 1.5f);

        if(ingredientSO.type == IngredientSO.IngredientType.BASE)
        {
            GetComponent<SpriteRenderer>().size = new Vector2(3f, 3f);
        }

        if(ingredientSO.type == IngredientSO.IngredientType.SPICE)
        {
            GetComponent<CircleCollider2D>().radius = 1f;
        }
    }

    private void Start()
    {
        SetSprite(ingredientSO.sprite);
        startPosition = transform.position;
    }
}
