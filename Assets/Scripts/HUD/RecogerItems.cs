using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerItems : MonoBehaviour
{

    public hud_refresh_items hudRefresh;

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto colisionado es el objeto que deseas recoger
       
               if (other.tag =="Player"){
            // Desactivar el objeto que ha sido recogido
            hudRefresh.sumarItem();
            Destroy(gameObject);
            }
    
}
}