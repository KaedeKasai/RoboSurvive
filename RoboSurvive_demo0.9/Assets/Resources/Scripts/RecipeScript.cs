using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeScript : MonoBehaviour
{
    public GameObject Material1;
    public GameObject Material2;
    public GameObject CreateObject;

    private CraftingScript _craftingScript;
    private List<GameObject> _recipeDetail = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        _craftingScript = GameObject.Find("CraftingScript").GetComponent<CraftingScript>();
        _recipeDetail.Add(Material1);
        _recipeDetail.Add(Material2);
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    public List<GameObject> GetRecipeMaterial()
    {
        //Debug.Log(CreateObject + "のレシピを読み込みました。");
        return _recipeDetail;

    }

    public GameObject GetCreateObject()
    {
        return CreateObject;
    }
}
