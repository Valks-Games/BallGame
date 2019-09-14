using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    private GameObject enemies;
    private GameObject player;

    private Camera mainCamera;
    private Transform mainCameraTransform;

    private GameObject gameObjectText;
    private Text levelText;

    private int level = 0;

    private void Start()
    {
        level = 1;
        gameObjectText = GameObject.Find("Wave");
        levelText = gameObjectText.GetComponent<Text>();
        mainCamera = Camera.main;
        mainCameraTransform = mainCamera.transform;
        enemies = GameObject.Find("Enemies");

        player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        player.name = "Player";

        StartCoroutine(SpawnEnemy());
        StartCoroutine(Wave());
    }

    private void LateUpdate()
    {
        Vector3 newCameraPos = player.transform.position - Vector3.forward * 10f;
        mainCameraTransform.position = newCameraPos;
    }

    private IEnumerator Wave()
    {
        while (true)
        {
            levelText.text = "Level " + level;
            StartCoroutine(FadeTextToZeroAlpha(2f, levelText));
            yield return new WaitForSeconds(6f);
            level++;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float distance = 15f;
            float angle = Random.Range(-Mathf.PI, Mathf.PI);

            Vector3 spawnPosition = player.transform.position;
            spawnPosition += new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;

            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.name = "Enemy";
            enemy.transform.parent = enemies.transform;

            yield return new WaitForSeconds(0.1f / level);
        }
    }
}
