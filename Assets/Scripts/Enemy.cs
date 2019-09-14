using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer sr;
    private float random;

    private void Start()
    {
        random = Mathf.Max(0, Random.value - 0.5f);
        if (Random.value < 0.01)
            random = 1;

        sr = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        StartCoroutine(Life());
    }

    private void FixedUpdate()
    {
        if (Random.value <= random)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 3f * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            SceneManager.LoadScene("Main");
        }
    }

    private IEnumerator Life()
    {
        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }
}
