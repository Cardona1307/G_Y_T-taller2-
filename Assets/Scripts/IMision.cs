public interface IMision
{
    // M�todo para obtener la descripci�n de la misi�n
    string ObtenerDescripcion();

    // M�todo para verificar si la misi�n est� completada
    bool EstaCompletada();

    // M�todo para intentar completar la misi�n (por ejemplo, cuando el jugador recoge todos los objetos)
    void IntentarCompletarMision();

    // M�todo para completar la misi�n manualmente (esto lo puede llamar el NPC o el MissionManager)
    void CompletarMision();
}
