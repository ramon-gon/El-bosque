using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhoulScript : MonoBehaviour
{
    public Animator animator;
    //public float Velocidad;
    public float grado;
    public Quaternion angulo;
    //public NavMeshAgent IA;

    public GameObject target;

    public AudioSource audioSource;
    public AudioClip[] musicClips;
    public float maxDistance = 10f;

    private bool isPlayingLoop;
    private int loopClipIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("PlayerArmature");
        //IA = GetComponent<NavMeshAgent>();

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        isPlayingLoop = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Perseguir
           //IA.SetDestination(target.transform.position);

           //animator.SetBool("run", true);
        
        //Perseguir
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

        animator.SetBool("run", true);
        transform.Translate(Vector3.forward * 2 * Time.deltaTime);

        //Música
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= maxDistance)
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
