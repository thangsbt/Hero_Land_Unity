using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGround : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigd;
    [SerializeField] float timedelay ;
    // Start is called before the first frame update
    void Start()
    {
        rigd = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(fall());
        }
    }
    IEnumerator fall()
    {
        yield return new WaitForSeconds(timedelay);
        rigd.bodyType = RigidbodyType2D.Dynamic;
        yield return 0;
    }
}
