using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollisionEnter : MonoBehaviour
{

    public int treeIndex;
    private float hard = 0.0f;
    private float timeOut = 1.0f;
    private float timeElapsed = 0.0f;


    

    public GameObject dropMaterial;

    // Use this for initialization
    void Start()
    {
        hard = 5.0f;
        timeOut = 1.0f;
        timeElapsed = 0.0f;
        dropMaterial = (GameObject)Resources.Load("Material_wood");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut)
        {
            if (hard <= 0)
            {
                destroy(collision);
            }
            hard--;
        }

    }

    private void destroy(Collision collision)
    {
        if (collision.gameObject.tag == "Tool")
        {
            
            Terrain terrain = Terrain.activeTerrain;
            List<TreeInstance> trees = new List<TreeInstance>(terrain.terrainData.treeInstances);
            trees[treeIndex] = new TreeInstance();
            terrain.terrainData.treeInstances = trees.ToArray();

            GameObject drop = Instantiate(dropMaterial, new Vector3(
                   this.gameObject.transform.position.x + 1,
                   this.gameObject.transform.position.y + 1,
                   this.gameObject.transform.position.z),
                   Quaternion.identity) as GameObject;

            drop.tag = "Material";
            drop.name = "Material_wood";

                   
            Destroy(this.gameObject);
        }
    }
}
