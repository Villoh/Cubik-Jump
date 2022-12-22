using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Player
    public GameObject player;
    //Obstaculos
    public GameObject[] obstacles;
    public GameObject obstaclesParent;
    
    //Montañas
    public GameObject[] mountains;
    public GameObject mountainsParent;

    //Arboles
    public GameObject[] trees;
    public GameObject treesParent;

    //Particulas
    public ParticleSystem obstacleParticles;
    public ParticleSystem treeParticles;
    public ParticleSystem mountainParticles;

    private float minRandomtime = 0.5F;
    private float maxRandomtime = 1F;

    private float spawnTime;
    private float startTime;
    private int randomSpawn;
    private int prefabType;

    private UIManager uiManager;

    void Start()
    {
        startTime = 1;
        //Corrutina de montañas y arboles.
        StartCoroutine(SpawnMountains());
        StartCoroutine(SpawnTrees());
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    /**
     * Corrutina que realiza el spawn de obstaculos esperando una cantidad aleatoria de tiempo al inicio y al final de esta, y en base a un entero aleatorio spawnea un prefab u otro.
     * IEnumerator SpawnObstacles()
     */
    private IEnumerator SpawnObstacles()
    {
        yield return new WaitForSeconds(startTime);
        startTime = Random.Range(minRandomtime, maxRandomtime);
        spawnTime = Random.Range(minRandomtime, maxRandomtime);

        //Bucle que itera una cantidad aleatoria de veces para realizar el spawn de obstaculos.
        randomSpawn = Random.Range(1, 4);
        for (int i = 0; i < randomSpawn; i++)
        {
            prefabType = Random.Range(0, 4);
            Instantiate(obstacles[prefabType], new Vector3(30 + i, obstacles[prefabType].transform.position.y, 0), Quaternion.identity, obstaclesParent.transform);
        }
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnObstacles());
    }

    /**
     * Corrutina que realiza el spawn de montañas esperando una cantidad aleatoria de tiempo al inicio y al final de esta, y en base a un entero aleatorio spawnea un prefab u otro.
     * IEnumerator SpawnMountains()
     */
    private IEnumerator SpawnMountains()
    {
        spawnTime = Random.Range(5F, 6f);
        prefabType = Random.Range(0, 4);
        Instantiate(mountains[prefabType], new Vector3(25, mountains[prefabType].transform.position.y, mountains[prefabType].transform.position.z), Quaternion.identity, mountainsParent.transform);
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnMountains());
    }

    /**
     * Corrutina que realiza el spawn de arboles esperando una cantidad aleatoria de tiempo al inicio y al final de esta, y en base a un entero aleatorio spawnea un prefab u otro.
     * IEnumerator SpawnMountains()
     */
    private IEnumerator SpawnTrees()
    {
        spawnTime = Random.Range(1F, 1.5F);
        prefabType = Random.Range(0, 4);
        Instantiate(trees[prefabType], new Vector3(25, trees[prefabType].transform.position.y, trees[prefabType].transform.position.z), Quaternion.identity, treesParent.transform);
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnTrees());
    }

    /**
     * Fin de juego
     */
    public void gameOver()
    {
        StartCoroutine(waitForParticle());
    }

    /**
     * Corrutina para esperar a que se reproduzcan las particulas correctamente
     * public IEnumerator waitForParticle()
     */
    public IEnumerator waitForParticle()
    {

        //yield return new WaitForSeconds(3F);
        StopAllCoroutines();
        destruyeFondo();
        if (PlayerPrefs.GetInt("counter") < uiManager.getScore())
        { 
            PlayerPrefs.SetInt("counter", uiManager.getScore());
        }
        uiManager.textosParado();
        StartCoroutine(SpawnMountains());
        StartCoroutine(SpawnTrees());
        yield return null;
    }

    //Deshabilita los botones y el titulo, y habilita la puntuación. Instancia un nuevo jugador.
    public void jugar()
    {
        uiManager.textosJugando();
        StartCoroutine(SpawnObstacles()); //Compieza la corrutina de obstaculos
        Instantiate(player, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity);
    }
    
    public void salir()
    {
        Application.Quit();
    }

    /**
     * Destruye todos los GameObjects pertenecientes al fondo e instancia un sistema de particulas por cada uno.
     * public void destruyeFondo()
     */
    public void destruyeFondo()
    {
        GameObject[] obstaculos = GameObject.FindGameObjectsWithTag("Obstaculo");
        foreach (GameObject obstaculo in obstaculos)
        {
            Instantiate(obstacleParticles, obstaculo.transform.position, Quaternion.identity);
            Destroy(obstaculo);
        }

        GameObject[] montañas = GameObject.FindGameObjectsWithTag("Montaña");
        foreach (GameObject montaña in montañas)
        {
            Instantiate(mountainParticles, montaña.transform.position, Quaternion.identity);
            Destroy(montaña);
        }

        GameObject[] arboles = GameObject.FindGameObjectsWithTag("Árbol");
        foreach (GameObject arbol in arboles)
        {
            Instantiate(treeParticles, arbol.transform.position, Quaternion.identity);
            Destroy(arbol);
        }
    }
}
