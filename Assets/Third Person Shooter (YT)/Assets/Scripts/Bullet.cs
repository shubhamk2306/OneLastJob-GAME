using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject impact;
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie")
        {
            GameObject _impact = Instantiate(impact, transform.position, Quaternion.identity);
            Destroy(_impact, 1f);
            other.GetComponent<ZombieEnemy>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
