using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{

    //Vari�vel geral de audio
    private AudioSource audioSource;

    //Vari�vel do controle de m�sica
    private float musicVolume = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = musicVolume;
    }

    //met�do que � chamado pelo slider
    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
