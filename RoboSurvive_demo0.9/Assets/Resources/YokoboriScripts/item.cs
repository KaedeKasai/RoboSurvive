using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    string[] stone = new string[] { "Rock2","Rock3","Rock4" };
    GameObject dropMaterial;
    GameObject dm;
    private float hard = 0.0f;
    private float hit = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        switch (this.gameObject.name) {
            case "rock1":
                hard = Random.Range(2,6);
            break;
            case "scrap":
                hard = 3;
                break;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool Probability(float fPer)
    {
        float fRate = UnityEngine.Random.value * 100.0f;

        if (fRate < fPer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public GameObject Drop(GameObject dropMaterial)
    {
        int x = Random.Range(0, 2);
        int z = Random.Range(0, 2);

       dm = Instantiate(dropMaterial, new Vector3(
                    transform.position.x+x,
                    transform.position.y,
                    transform.position.z+z),
                    Quaternion.identity);

        return dm;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Tool")
        {
            hit++;

            if (hit >= hard)
            {
                
                    switch (this.name)
                    {
                        case "rock1":
                        if (hard <= 0)
                        {
                            dropMaterial = (GameObject)Resources.Load(stone[0]);
                            Drop(dropMaterial);
                            dm.name = "rock";
                            if (Probability(60))
                            {
                                dropMaterial = (GameObject)Resources.Load(stone[1]);
                                Drop(dropMaterial);
                                dm.name = "stone";
                            }
                            if (Probability(30))
                            {
                                dropMaterial = (GameObject)Resources.Load(stone[2]);
                                Drop(dropMaterial);
                                dm.name = "FrakedStone";
                            }
                            Destroy(this.gameObject);
                        }
                            break;
                    }
                hard--;
            }
                
            
            
        }
    }
}
