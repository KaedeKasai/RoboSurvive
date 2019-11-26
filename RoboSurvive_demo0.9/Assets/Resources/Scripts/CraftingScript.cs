using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UI(ボタンやUI関連のライブラリを扱いたい時は以下を記述)
using UnityEngine.UI;


public class CraftingScript : MonoBehaviour
{



    //現在
    private List<GameObject> _CanCraftMaterialList = new List<GameObject>();
    private Button _craftButton;
    private GameObject _workbench;

    void Start()
    {
        //"Workbench"と"CraftButton"をシーン内で検索
        _workbench = GameObject.Find("Workbench");
        _craftButton = GameObject.Find("CraftButton").GetComponent<Button>();

        //ボタンを押されるとOnClickButtonを実行
        _craftButton.onClick.AddListener(OnClickButton);
    }

    //void Update()
    //{  
    //}


    void OnClickButton()
    {
        //recipeListに登録されているレシピから作れるものを検索
        GameObject recipeList = GameObject.Find("RecipeList");
        List<string> _CanCraftMaterialString = new List<string>();

        //上で検索したものを_CanCraftMaterialStringに格納
        foreach (GameObject gameObject in _CanCraftMaterialList)
        {
            _CanCraftMaterialString.Add(GetMaterialName(gameObject));
        }

        Debug.Log("Crafting now!");

        //オブジェクト生成スクリプト
        foreach (Transform recipe in recipeList.transform)
        {


            RecipeScript recipeScript = recipe.gameObject.GetComponent<RecipeScript>();
            List<GameObject> recipeMaterialList = recipeScript.GetRecipeMaterial();

            bool existsMaterial = true;



            foreach (GameObject recipeMaterial in recipeMaterialList)
            {
                //Debug.Log(_CanCraftMaterialString.Contains(recipeMaterial.transform.GetComponentInChildren<MaterialScript>().MaterialName));

                if (!_CanCraftMaterialString.Contains(recipeMaterial.transform.GetComponentInChildren<MaterialScript>().MaterialName))
                {
                    existsMaterial = false;
                }
            }

            if (existsMaterial)
            {
                //Debug.Log("現在、" + recipeScript.GetCreateObject().name + "がクラフト可能です");



                Destroy(_CanCraftMaterialList[0].transform.parent.gameObject);
                Destroy(_CanCraftMaterialList[1].transform.parent.gameObject);

                _CanCraftMaterialList = new List<GameObject>();

                Instantiate(recipeScript.GetCreateObject(), new Vector3(
                    _workbench.transform.position.x + 1,
                    _workbench.transform.position.y + 1,
                    _workbench.transform.position.z),
                    Quaternion.identity);


                Debug.Log(recipeScript.GetCreateObject().name + "をクラフトしました。");



            }
            else
            {


            }


        }


    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Material")
        {
            //三つ目の素材を無視する
            //if (_CanCraftMaterialList.Count <= 2)
            //{
            //    _CanCraftMaterialList.Add(other.gameObject);
            //}
            //else
            //{
            //    Debug.Log("3つ目の要素は追加できません");
            //}

            _CanCraftMaterialList.Add(other.gameObject);

            ListContentToConsole();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Material")
        {
            _CanCraftMaterialList.Remove(other.gameObject);


            ListContentToConsole();
        }
    }

    void ListContentToConsole()
    {

        string listContents = "";

        foreach (GameObject Material in _CanCraftMaterialList)
        {
            listContents += GetMaterialName(Material) + ",";
        }

        Debug.Log("マテリアル対象:" + listContents);
    }

    string GetMaterialName(GameObject material)
    {
        return material.GetComponent<MaterialScript>().MaterialName;
    }



}