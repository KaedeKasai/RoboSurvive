using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SackScript : MonoBehaviour
{

    public GameObject BreakFX;
    private GameObject _parent;

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
        if (other.gameObject.tag == "Material")

        {
            Instantiate(BreakFX, new Vector3(
                    other.transform.position.x,
                    other.transform.position.y,
                    other.transform.position.z), Quaternion.identity);

            Destroy(_parent);

        }

    }

}
