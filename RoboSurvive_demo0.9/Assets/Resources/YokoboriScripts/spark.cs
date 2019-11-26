using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spark : MonoBehaviour
{

    //tag.Env == rock&scrap
    //tag.tree == tree

    private GameObject effect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Env")
        {
            switch (collision.gameObject.name)
            {
                case "rock1":
                    effect = (GameObject)Resources.Load("CFX2_SparksHit_B Sphere");
                    break;
                case "tree":
                    effect = (GameObject)Resources.Load("CFX3_Hit_SmokePuff");
                    break;
                default:
                    break;
            }


            foreach (ContactPoint colPoint in collision.contacts)
                Instantiate(effect, colPoint.point, Quaternion.identity);
        }
    }
}
