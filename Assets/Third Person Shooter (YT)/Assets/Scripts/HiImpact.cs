using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiImpact : MonoBehaviour
{
    public GameObject[] gore;
    // Start is called before the first frame update
    void Start()
    {
        int _count = Random.Range(1, 4);
        for (int i = 0; i < _count; i++)
        {
            GameObject _gore = Instantiate(gore[Random.Range(0, gore.Length)], transform.position, Quaternion.identity);
            _gore.transform.localScale *= Random.Range(0.2f, 0.6f);
            _gore.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(1, 3), Random.Range(1, 3), Random.Range(1, 3)), ForceMode.Impulse);
            Destroy(_gore, 3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
