using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject[] armaduras; 

    public void DesactivarArmadura(int indice){
        armaduras[indice].SetActive(false);
    }

    public void ActivarArmadura(int indice){
        armaduras[indice].SetActive(true);
    }
}
