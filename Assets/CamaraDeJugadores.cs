using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraDeJugadores : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = (int)(mPosition.x + 0.5);
        int y = (int)(mPosition.y + 0.5);
        
    }
}
