using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class GhoulScript : MonoBehaviour
{
    public GameObject warrior;
    public GameObject enemy;
    public GameObject[] spawns;
    public GameObject selectedRespawn;
    public armorManager armor_manager;
    public Animator animator;
    public NavMeshAgent IA;
    

    public AudioSource audioSource;
    public AudioClip[] musicClips;
    public float maxDistance = 10f;

    private bool isPlayingLoop;
    private int loopClipIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializar personajes
        animator = GetComponent<Animator>();
        warrior = GameObject.Find("PlayerArmature");
        enemy = GameObject.Find("Enemigo_IA");
        spawns = GameObject.FindGameObjectsWithTag("Respawn");
        selectedRespawn = GameObject.Find("Respawn");
        IA = GetComponent<NavMeshAgent>();

        //Inicializar música
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        isPlayingLoop = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Perseguir
        if(warrior!=null && IA!=null){
            IABehaviour();
        } else {
            Debug.Log("El Jugador o el Enemigo són null!");
        }

        //Música
        float distanceTowarrior = Vector3.Distance(transform.position, warrior.transform.position);

        if (distanceTowarrior <= maxDistance)
        {
            if (!audioSource.isPlaying)
            {
                if (!isPlayingLoop)
                {
                    PlayRandomMusicClip();
                }
                else
                {
                    PlayLoopMusicClip();
                }
            }
        }
        else
        {
            audioSource.Stop();
            isPlayingLoop = false;
        }
    }

    //Función IA comportamiento
    void IABehaviour()
    {
        //Si la distancia es mayor a 1 persigue al jugador
        if(Vector3.Distance(transform.position, warrior.transform.position) > 1){
            IAStalckPlayer();

        //De lo contrario ataca al jugador + respawn
        } else {
            IAAttackPlayer();

            PerderArmadura();
            
            toSpawn();

        }
        
    }

    //Función IA persigue a Jugador
    void IAStalckPlayer()
    {
        animator.SetBool("attack", false);
        IA.SetDestination(warrior.transform.position);

        var lookPos = warrior.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

        animator.SetBool("run", true);
        transform.Translate(Vector3.forward * 2 * Time.deltaTime);
    }

    //Función IA ataca a Jugador
    void IAAttackPlayer()
    {
        animator.SetBool("run", false);
        animator.SetBool("attack", true);
    }

    //Función respawn de IA
    void toSpawn()
    {
       if(spawns.Length > 0)
       {
     
           IA.enabled = false;
            int indexRandom = UnityEngine.Random.Range(0, spawns.Length);

            selectedRespawn = spawns[indexRandom];

            Vector3 spawnPosition =  selectedRespawn.transform.position;

        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10f, NavMesh.AllAreas))
        {
            transform.position = navMeshHit.position;
        }
            IA.enabled = true;
          
       }
    }

    //Función perderArmadura
    private void PerderArmadura(){
        armorManager.armaduras -= 1;

        if (armorManager.armaduras == 0)
        {
            //Animación de muerte
            ThirdPersonController.morir = true;
            //SceneManager.LoadScene(0);
        }

        armor_manager.hud.DesactivarArmadura(armorManager.armaduras);
    }

    

    void PlayRandomMusicClip()
    {
        if (musicClips.Length == 0)
        {
            Debug.LogWarning("No se han asignado clips de música al GhoulScript.");
            return;
        }

        int randomIndex = Random.Range(0, musicClips.Length);
        audioSource.clip = musicClips[randomIndex];
        audioSource.Play();

        loopClipIndex = (randomIndex + 1) % musicClips.Length;
        isPlayingLoop = true;
    }

    void PlayLoopMusicClip()
    {
        audioSource.clip = musicClips[loopClipIndex];
        audioSource.Play();
        loopClipIndex = (loopClipIndex + 1) % musicClips.Length;
    }
}
