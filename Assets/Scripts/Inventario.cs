using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public List<string> objetos = new List<string>(); // Lista de objetos en el inventario

    // Agrega un objeto al inventario
    public void AgregarObjeto(string nombreObjeto)
    {
        objetos.Add(nombreObjeto);
        Debug.Log($"Objeto {nombreObjeto} agregado al inventario.");
    }

    // Verifica si el jugador tiene un objeto espec�fico en el inventario
    public bool TieneObjeto(string nombreObjeto)
    {
        return objetos.Contains(nombreObjeto); // Devuelve true si el objeto est� en el inventario
    }

    // Elimina todos los objetos del inventario
    public void LimpiarInventario()
    {
        objetos.Clear();
        Debug.Log("Inventario limpiado.");
    }

    // M�todo para obtener todos los objetos actuales en el inventario (opcional)
    public List<string> ObtenerObjetos()
    {
        return objetos;
    }
}
