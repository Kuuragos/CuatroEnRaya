using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaDelJuego : MonoBehaviour
{
    //variables que se usan
    public int width = 10;
    public int height = 10;
    int turnos = 0;
    GameObject[,] matriz; 
    public GameObject gama;
    //variables de color
    public Color baseColor;
    public Color jugador1;
    public Color jugador2;
    public Color iA;
    // variables que indican el turno del jugador y en el turno en el que estan
    public TextMesh m3Dtext;
    public TextMesh ronda;
    public float distance = 0;
    // Vatiables de control del juego y los jugadores
    bool turno;
    bool gameOver= true;
    void Start()
    {
        // creacion de esferas en el tablero
        matriz = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject esfera = GameObject.CreatePrimitive(PrimitiveType.Sphere) as GameObject;
                esfera.GetComponent<Renderer>().material.color = baseColor;
                esfera.transform.position = new Vector3(x * distance, y * distance, 0f); //separan las esferar con una distancia de 1.5
                matriz[x, y] = esfera;
            }
        }
    }  
    void Update()
    {
        if(gameOver == true) // controla el juego
        {
            Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // muestra el jugador en turno
            if (!turno)
            {
                m3Dtext.text = "Jugador = 1.";
            }
            else
                m3Dtext.text = "Jugador = 2.";
            ronda.text = ("Turno = " + turnos);
            PintarFicha(mPosition);
        }
    }
    void PintarFicha(Vector3 position)
    {
        // Colorea la esfera en la pocicion (X,Y)
        int x = (int)((position.x + 0.5f) / distance);
        int y = (int)((position.y + 0.5f) / distance);
        if (Input.GetMouseButtonDown(0)) //Input que verifica si el usuario dio click isquierdo
        {
            if (x >=0 && y >=0 && x < width && y< height) // Este "SI" Verifica que las bariables "X" y "Y" esten en el rango de la matriz
            {
                GameObject esfera = matriz[x, y];
                if (esfera.GetComponent<Renderer>().material.color == baseColor) // Verifica que la esfera en la que se le da click sea del mismo color de la base 
                {
                    Color colorAUsar = Color.clear; // Variable temporal para almacenar el color y comporarlo más adelante
                    if (!turno)
                    {
                        colorAUsar = jugador1;
                        esfera.GetComponent<Renderer>().material.color = jugador1;
                        turno = true;
                        Verificar(x, y, colorAUsar);
                        Verificar2(x, y, colorAUsar);
                        turnos++;
                    }
                    else 
                    {
                        colorAUsar = jugador2;
                        esfera.GetComponent<Renderer>().material.color = jugador2;
                        turno = false;
                        Verificar(x, y, colorAUsar);
                        Verificar2(x, y, colorAUsar);
                        turnos++;
                    }
                    if (turnos % 2 == 0) // Al cumplir la condicion entrara en la regla inplementada en el juego
                    {
                        int azar = Random.Range(0, 3);
                        switch (azar) // Swith que verifica el numero dado anterior.
                        {
                            case 0: // Primera opcion
                                int n = 1; // Numero de regla a implementar
                                Regla(n);
                                break;
                            case 1: // Segunda opcion
                                int nn = 2; // Numero de regla a implementar
                                Regla(nn);
                                break;
                            case 2: // Tercera opcion
                                int m = 3; // Numero de regla a implementar
                                Regla(m);
                                break;
                        }
                    }
                }
                
            }
        }
    }
    // Verificador en cruz 
    public void Verificar(int x, int y, Color colorVerificar)
    {
        int gana, verifica;
        verifica = 0; // indica verificador 0 para horizontal y 1 para vertical
        gana = 0; // Acumulador de ganador 
        //Verifica en horizontal
        if (verifica == 0)
        {
            for (int i = x - 3; i < x + 4; i++) // Rebisara 3 casillas a la Izquierda y 3 a la derecha
            {
                if (i < 0 || i >= width) // Verifica que la variable i cumpla los requisitos 
                    continue;  // Continua con la siguiente iteración
                GameObject esfera = matriz[i, y];
                if (esfera.GetComponent<Renderer>().material.color == colorVerificar)
                {
                    gana++;
                    if (gana == 4) // Verifica que la variable gana cumpla con el requisito para finalisar el juego
                    {
                        gama.SetActive(true); // Prende el letrero al detectar el ganador
                        gameOver = false; // finalisa el juego
                    }
                }
                else
                {
                    gana = 0;
                }
            }
            verifica++;
            gana = 0;
        }
        //Verifica en vertical
        if (verifica ==1)
        {
            for (int j = y - 3; j < y + 4; j++) // Rebisara 3 casillas a la Izquierda y 3 a la derecha
            {
                if (j < 0 || j >= height) // Verifica que la variable j cumpla los requisitos 
                    continue;
                GameObject esfera = matriz[x, j];
                if (esfera.GetComponent<Renderer>().material.color == colorVerificar)
                {
                    gana++;
                    if (gana == 4)
                    {
                        gama.SetActive(true); 
                        gameOver = false;
                    }
                }
                else
                {
                    gana = 0;
                }
            }
            gana = 0;
        }
    }
    // Verificador en Equis
    public void Verificar2(int X, int Y, Color colorVerificar)
    {
        int gana, verifica;
        verifica = 0;
        gana = 0;
        //diagonal ariba
        if (verifica == 0)
        {
            int j = Y - 3; // Variable que hace funsion de Y
            for (int i = X - 3; i <= X + 3; i++)
            {
                if ((i >= 0 && i < width) && (j >= 0 && j < height))
                {
                    GameObject esfera = matriz[i, j];
                    if (esfera.GetComponent<Renderer>().material.color == colorVerificar)
                    {
                        // print("Esfera que compara es:" + esfera.GetComponent<Renderer>().material.color + "color de jugador" + colorVerificar);
                        gana++;
                        if (gana == 4)
                        {
                            gama.SetActive(true);
                            gameOver = false;
                        }
                    }
                    else
                    {
                        gana = 0;
                    }
                }
                if (j < 0 || j < width)
                    j++;
            }
            verifica++;
            gana = 0;
        }
        // diagonal abajo
        if (verifica ==1)
        {
            int k = Y + 3; // Variable que hace funsion de Y
            for (int i = X - 3; i <= X + 3; i++)
            {
                if ((i >= 0 && i < width) && (k >= 0 && k < height))
                {
                    GameObject esfera = matriz[i, k];
                    if (esfera.GetComponent<Renderer>().material.color == colorVerificar)
                    {
                        gana++;
                        if (gana == 4)
                        {
                            gama.SetActive(true);
                            gameOver = false;
                        }
                    }
                    else
                    {
                        gana = 0;
                    }
                }
                if (k <0 || k<= width)
                k--;
            }
            gana = 0;
        }
    }
    public void Regla(int m)
    {
        // Menu de las reglas 
        if (m == 1)
        {
            int x = Random.Range(0, 10);
            int y = Random.Range(0, 10);
            int i = Random.Range(0, 10);
            int j = Random.Range(0, 10);
            GameObject esfera = matriz[x, y];
            GameObject esfera2 = matriz[i, j];
            if (esfera.GetComponent<Renderer>().material.color == baseColor)
            {
                esfera.GetComponent<Renderer>().material.color = iA;
                esfera2.GetComponent<Renderer>().material.color = iA;
            }
        }
        else if (m==2)
        {
            int randon = Random.Range(0, 2);
            print("Randon es:"+ randon);
            switch (randon) // Swith que verifica el numero dado anterior.
            {
                case 0: // Primera opcion
                    turno = false;
                    break;
                case 1: // Segunda opcion
                    turno = true;
                    break;
            }
        }
        else
        {
            int x = Random.Range(0, 10);
            int y = Random.Range(0, 10);
            int i = Random.Range(0, 10);
            int j = Random.Range(0, 10);
            int g = Random.Range(0, 10);
            int h = Random.Range(0, 10);
            GameObject esfera = matriz[x, y];
            GameObject esfera2 = matriz[i, j];
            GameObject esfera3 = matriz[g, h];
            if (esfera.GetComponent<Renderer>().material.color == baseColor)
            {
                esfera.GetComponent<Renderer>().material.color = iA;
                esfera2.GetComponent<Renderer>().material.color = iA;
                esfera3.GetComponent<Renderer>().material.color = iA;
            }
        }
        // Apesar de que la primera y la tersera son iguales las tengo haci para poder tener bariedad de posibilidad
    }
}