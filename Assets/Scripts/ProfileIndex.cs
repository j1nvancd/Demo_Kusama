using System.Collections.Generic;
//Indice creado para listar todas las partidas que hay almacenadas
public class ProfileIndex
{
    //Lista con los nombres de los perfiles creados en el índice
    public List<string> profileFileNames;

    //Llamamos al index donde se almacenan los nombres de los archivos de juego
    public ProfileIndex()
    {
        this.profileFileNames = new List<string>();
    }
}
