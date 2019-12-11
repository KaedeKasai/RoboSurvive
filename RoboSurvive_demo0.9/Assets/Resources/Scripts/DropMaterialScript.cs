using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMaterialScript : MonoBehaviour
{

    public GameObject dropMaterial;
    public int _durability;

    private int hitTimes = 0;
    private GameObject _parent;
    public GameObject BreakFX;




    // Start is called before the first frame update
    void Start()
    {
        _parent = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tool")

            

        {
            //Instantiate(BreakFX, new Vector3(
                    //other.transform.position.x,
                   // other.transform.position.y,
                    //other.transform.position.z), Quaternion.identity);

            hitTimes += 1;
            if (_durability <= hitTimes)
            {
                Instantiate(dropMaterial, new Vector3(
                    _parent.transform.position.x + 1,
                    _parent.transform.position.y + 1,
                    _parent.transform.position.z),
                    Quaternion.identity);
                Destroy(_parent);
            }

            Debug.Log("HIT! 残り耐久度:" + (_durability - hitTimes));
        }
        
    }

}
