using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armadura : MonoBehaviour
{
    public armorManager armor_manager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool armaduraRecuperada = RecuperarArmadura();
            
            if(armaduraRecuperada){
                Destroy(this.gameObject);
            }
        }
    }

    //Función recuperarArmadura
    private bool RecuperarArmadura(){
        if (armorManager.armaduras == 3)
        {
            return false;
        }

        armor_manager.hud.ActivarArmadura(armorManager.armaduras);
        armorManager.armaduras += 1;
        return true;
    }
}
