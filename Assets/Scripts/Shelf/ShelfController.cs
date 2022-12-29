using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfController : MonoBehaviour
{
    public GameObject[] shelfSlots;
    public GameObject ingredientPrefab;
    public List<IngredientSO> ingredientListInShelf;
    public List<GameObject> ingredientObjectsInShelf;

    private void Start()
    {
        PopulateShelfWithItems();
    }
    public void AddIngredientToShelf(IngredientController ingredient)
    {
        for(int i = 0; i < shelfSlots.Length; i++)
        {
            if(shelfSlots[i].transform.childCount == 0)
            {
                ingredient.transform.parent = shelfSlots[i].transform;
                ingredient.transform.position = shelfSlots[i].transform.position;
            }
        }
    }

    public void PopulateShelfWithItems()
    {
        foreach(IngredientSO ingredient in ingredientListInShelf)
        {
            GameObject ingredientObject = Instantiate(ingredientPrefab);
            ingredientObject.name = ingredient.ingredientName;
            ingredientObject.transform.position = new Vector3(ingredientObject.transform.position.x, ingredientObject.transform.position.y, -1);
            ingredientObjectsInShelf.Add(ingredientObject);
            ingredientObject.GetComponent<IngredientController>().ingredientSO = ingredient;
        }

        foreach(GameObject ingredientObject in ingredientObjectsInShelf)
        {
            AddIngredientToShelf(ingredientObject.GetComponent<IngredientController>());
        }
    }
}
