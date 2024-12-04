public interface IMision
{
    // Método para obtener la descripción de la misión
    string ObtenerDescripcion();

    // Método para verificar si la misión está completada
    bool EstaCompletada();

    // Método para intentar completar la misión (por ejemplo, cuando el jugador recoge todos los objetos)
    void IntentarCompletarMision();

    // Método para completar la misión manualmente (esto lo puede llamar el NPC o el MissionManager)
    void CompletarMision();
}
