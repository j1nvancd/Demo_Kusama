public class ProfileData
{   
    //Nombre del archivo a guardar
    public string filename;
    //Nombre del perfil
    public string name;
    //Booleano que nos dice si es un juego nuevo o no
    public bool newGame;

    //Posici�n del jugador
    public float x;
    public float y;

    //Llamamos al ProfileData de abajo
    public ProfileData()
    {
        this.filename = "None.xml";
        this.name = "None";
        this.newGame = false;

        this.y = this.x = 0;
    }

    //Constructor que recibe el nombre del perfil, si es nuevo o no y la posici�n del jugador
    public ProfileData(string name, bool newGame, float x, float y)
    {
        //formato del nombre del archivo que se ver� en el men� de carga
        this.filename = name.Replace(" ", "_") + ".xml";
        this.name = name;
        this.newGame = newGame;
        this.x = x;
        this.y = y;
    }
}