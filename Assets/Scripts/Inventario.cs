using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public List<string> objetos = new List<string>(); // Lista de nombres de objetos en el inventario

    public void AgregarObjeto(string nombreObjeto)
    {
        objetos.Add(nombreObjeto);
        UnityEngine.Debug.Log($"Objeto {nombreObjeto} agregado al inventario.");
    }
}