using System.Collections;
using UnityEngine;

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

    private float minRandomtime;
    private float maxRandomtime;

    private float spawnTime;
    private float startTime;
    private int randomSpawn;
    private int prefabType;
    private int velocidadScroll;

    private UIManager uiManager;
    private AudioSource[] audioSources;

    void Start()
    {
        //Corrutina de montañas y arboles.
        StartCoroutine(SpawnMountains());
        StartCoroutine(SpawnTrees());

        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>(); //Busca en UIManager en la escena.
        audioSources = gameObject.GetComponents<AudioSource>();
    }

    /**
     * IEnumerator SpawnObstacles()
     * Corrutina que realiza el spawn de obstaculos esperando una cantidad aleatoria de tiempo al inicio y al final de esta, y en base a un entero aleatorio spawnea un prefab u otro.
     * Método recursivo que se llama así mismo para conseguir que se instancien obstáculos constantemente.
     */
    private IEnumerator SpawnObstacles()
    {
        curvaDificultad();
        yield return new WaitForSeconds(startTime);
        startTime = Random.Range(minRandomtime, maxRandomtime);
        spawnTime = Random.Range(minRandomtime, maxRandomtime);
        //Bucle que itera una cantidad aleatoria de veces para realizar el spawn de obstaculos.
        randomSpawn = Random.Range(1, 4);
        for (int i = 0; i < randomSpawn; i++)
        {
            prefabType = Random.Range(0, 4);
            EndlessScroll endlessScroll = Instantiate(obstacles[prefabType], new Vector3(30 + i, obstacles[prefabType].transform.position.y, 0), Quaternion.identity, obstaclesParent.transform).GetComponent<EndlessScroll>();
            endlessScroll.velocidadJuego = velocidadScroll;
        }
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnObstacles());
    }

    /**
     * IEnumerator SpawnMountains()
     * Corrutina que realiza el spawn de montañas esperando una cantidad aleatoria de tiempo al inicio y al final de esta, y en base a un entero aleatorio spawnea un prefab u otro.
     * Método recursivo que se llama así mismo para conseguir que se instancien montañas constantemente.
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
     * IEnumerator SpawnTrees()
     * Corrutina que realiza el spawn de arboles esperando una cantidad aleatoria de tiempo al inicio y al final de esta, y en base a un entero aleatorio spawnea un prefab u otro.
     * Método recursivo que se llama así mismo para conseguir que se instancien arboles constantemente.
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
     * public IEnumerator waitForParticle()
     * Fin de juego.
     * Método que para todas las corrutinas que se están reproduciendo, destruye todos los objetos del fondo, guarda la puntación si es más alta que la existente, activa los textos pertinentes llamando al metodo del UIManager y comienza nuevas corrutinas para los objetos del fondo.    
     */
    public void gameOver()
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn(audioSources[0], 2F));
        audioSources[1].Play();
        destruyeFondo();
        if (PlayerPrefs.GetInt("counter") < uiManager.getScore())
        { 
            PlayerPrefs.SetInt("counter", uiManager.getScore());
        }
        uiManager.textosJuegoPausado();
        StartCoroutine(SpawnMountains());
        StartCoroutine(SpawnTrees());
    }

    //Deshabilita los botones y el titulo, y habilita la puntuación. Instancia un nuevo jugador.
    public void jugar()
    {
        uiManager.textosComienzaJuego();
        StartCoroutine(FadeOut(audioSources[0], 2F));
        StartCoroutine(SpawnObstacles()); //Compieza la corrutina de obstaculos
        Instantiate(player, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity);
        
    }
    
    //Sale de la aplicación.
    public void salir()
    {
        Application.Quit();
    }

    /**
     * public void destruyeFondo()
     * Destruye todos los GameObjects pertenecientes al fondo e instancia un sistema de particulas por cada uno.
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

    /**
     * public int dificultadJuego() 
     * Devuelve un entero en base a la puntuación, este entero será la dificultad del juego
     */
    private int tipoDificultad() 
    {
        int dificultad = 0;

        if (uiManager.getScore() >= 2500 && uiManager.getScore() < 5000)
        {
            dificultad = 1;

        }
        else if (uiManager.getScore() >= 5000 && uiManager.getScore() < 7500)
        {
            dificultad = 2;
        }
        else if (uiManager.getScore() >= 7500 && uiManager.getScore() < 10000)
        {
            dificultad = 3;
        }
        else if (uiManager.getScore() >= 10000)
        {
            dificultad = 4;
        }
        return dificultad;
    }

    /**
     * public void curvaDificultad()
     * En base al tipoDificultad se realizan una serie de ajustes en la aleatoriedad de los spawns y en la velocidad que va a tener el objeto
     */
    public void curvaDificultad()
    {
        switch (tipoDificultad())
        {
            case 0:
                minRandomtime = 0.5F;
                maxRandomtime = 1F;
                velocidadScroll = 15;
                break;
            case 1:
                minRandomtime = 0.5F;
                maxRandomtime = 0.8F;
                velocidadScroll = 17;
                break;
            case 2:
                minRandomtime = 0.4F;
                maxRandomtime = 0.7F;
                velocidadScroll = 18;
                break;
            case 3:
                minRandomtime = 0.4F;
                maxRandomtime = 0.6F;
                velocidadScroll = 19;
                break;
            case 4:
                minRandomtime = 0.4F;
                maxRandomtime = 0.5F;
                velocidadScroll = 20;
                break;
            default: Debug.Log("No existe este tipo de dificultad");
                break;
        }
    }

    /**
     * public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
     * Corrutina que se encarga de realizar la transicion de volumen, de más a menos
     */
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        yield return new WaitForSeconds(2F);
        float startVolume = 1F;

        while (audioSource.volume > 0.06F)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
    }

    /**
     * public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
     * Corrutina que se encarga de realizar la transicion de volumen, de menos a más
     */
    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 1F;
        while (audioSource.volume < 0.5f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
    }
}
