using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTerrain : MonoBehaviour
{

    private Terrain terrain;
    private TreeInstance[] _originalTrees;

    void Start()
    {

        terrain = GetComponent<Terrain>();
        _originalTrees = terrain.terrainData.treeInstances;

        // Treeの位置にCapsuleを生成する
        for (int i = 0; i < terrain.terrainData.treeInstanceCount; i++)
        {

            // TreeInstanceの取得
            TreeInstance treeInstance = terrain.terrainData.treeInstances[i];
            

            // Capsuleの生成
            GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            capsule.tag = "Env";
            capsule.name = "tree";
            capsule.transform.position = Vector3.Scale(treeInstance.position, terrain.terrainData.size) + Terrain.activeTerrain.transform.position;
            capsule.transform.parent = terrain.transform;
            capsule.GetComponent<MeshRenderer>().enabled = false;

            // CapsuleのColliderを設定
            CapsuleCollider capsuleCollider = capsule.GetComponent<CapsuleCollider>();
            capsuleCollider.center = new Vector3(0, 5, 0);
            capsuleCollider.height = 10;
            capsuleCollider.radius = 0.5f;

            // CapsuleにComponentを追加
            TreeCollisionEnter tree = capsule.AddComponent<TreeCollisionEnter>();
            tree.treeIndex = i;
        }
    }

    // TreeInstanceを保存する
    void OnApplicationQuit()
    {

        terrain.terrainData.treeInstances = _originalTrees;
    }
}


