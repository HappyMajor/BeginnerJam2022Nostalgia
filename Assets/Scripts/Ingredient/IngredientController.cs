using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    public IngredientSO ingredientSO;

    private bool selected = false;
    private CocktailController collidingWithCocktail = null;

    public void Select()
    {
        this.selected = true;
    }

    public void DeSelect()
    {
        this.selected = false;
        if(collidingWithCocktail)
        {
            PourIntoCocktail(collidingWithCocktail);
        }
    }

    public void PourIntoCocktail(CocktailController cocktail)
    {
        if(ingredientSO.type == IngredientSO.IngredientType.BASE)
        {
            cocktail.AddBase(this,33);
            gameObject.SetActive(false);
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
        if(collision.gameObject.GetComponent<CocktailController>() != null)
        {
            collidingWithCocktail = collision.gameObject.GetComponent<CocktailController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.Equals(collidingWithCocktail))
        {
            collidingWithCocktail = null;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        GetComponent<SpriteRenderer>().size = new Vector2(1, 1);
    }

    private void Start()
    {
        SetSprite(ingredientSO.sprite);
    }
}
