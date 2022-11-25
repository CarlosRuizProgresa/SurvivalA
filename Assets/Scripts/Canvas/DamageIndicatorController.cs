using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicatorController : MonoBehaviour
{
    //Variables 
    public Image image;

    public float fadeSpeed;
    
    //Para controlar el estado de la Corutina
    // guardo su ejecucion en un variable
    private Coroutine fadeAway;
    
    //Un metodo que llamaremos de PlayerNeeds
    // cada vez que se ejecute el metodo TakeDamage
    // para inicar un corutina que mostrar el 
    // DamageIndicator
    public void Fade()
    {
        //Compruebo si hay un corutina ejecutandose
        if (fadeAway != null)
        {
            StopCoroutine(fadeAway);
        }
        //Empezamos una nueva
        image.enabled = true;
        image.color = Color.white;
        //TODO: ejecutaremos la corutina
        fadeAway = StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        //Establecemos una variables para 
        // guardar el alpha inicial
        float a = 1.0f;
        //Bajamos el alpha hasta 0 en un tiempo determinado
        while (a > 0.0f)
        {
            a -= (1.0f / fadeSpeed) * Time.deltaTime;
            //Establcer en la imagen el nuevo alpha
            image.color = new Color(1, 1, 1, a);
            yield return null;
        }

        image.enabled = false;
    }
}
