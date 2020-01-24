using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider))]

public class MaterialScript : MonoBehaviour
{
    //BoxCollider bc;
    [SerializeField]

    private string MaterialName;
    private string MaterialNameKey;
    
    // Start is called before the first frame update
    void Start()
    {
        //bc = GetComponent<BoxCollider>();
        //bc.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public string GetName()
    {
        return MaterialName;
    }

    public string GatMaterialNameKey()
    {
        return MaterialNameKey;
    }

    

}
