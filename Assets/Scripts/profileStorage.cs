using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEditorInternal;

public class profileStorage: MonoBehaviour
{
    //Referencia estática que contenga los datos del perfil usados en ese momento
    public static ProfileData s_currentProfile;

    //Variable estática con la ruta donde estará el archivo de lista de partidas y cada partida por separado
    private static string s_indexpath = Application.streamingAssetsPath + "/profiles/_ProfileIndex_.xml";

    //Funcion para crear un nuevo juego
    public static void CreateNewGame(string profileName)
    {
        //Generamos unos datos de perfil nuevos con este formato (Nombre de la partida, si ees un juego nuevo o no, posición x, posición y)
        s_currentProfile = new ProfileData(profileName, true, 0, 0);

        string path = Application.streamingAssetsPath + "/Profiles/" + s_currentProfile.filename;
        SaveFile<ProfileData>(path, s_currentProfile);
        
        //Obtención del índice
        var index = GetProfileIndex();
        //Cuando creamos un nuevo juego añadimos el nombre del archivo de ese juego al índice
        index.profileFileNames.Add(s_currentProfile.filename);
        //Cogemos el índice que he dicho antes y guardamos el archivo creado
        SaveFile<ProfileIndex>(s_indexpath, index);
    }

    //Función que obtiene el índice actual
    public static ProfileIndex GetProfileIndex()
    {
        //Si el índice actual no existe
        if (File.Exists(s_indexpath) == false)
        {
            //Se crea un nuevo índice
            return new ProfileIndex();
        }
        //Si existe cargamos el índice del archivo
        return LoadFile<ProfileIndex>(s_indexpath);
    }

    //Para cargar el perfil recibe el nombre del archivo que queremos cargar
    public static void LoadProfile(string filename)
    {
        //Pillamos la ruta de ese archivo a cargar
        var path = Application.streamingAssetsPath + "/Profiles/" + filename;
        //Carga el archivo
        s_currentProfile = LoadFile<ProfileData>(path);
    }

    //Funcion para guardar los datos
    public static void StorePlayerProfile(GameObject player)
    {
        //Asignamos la posicion del jugador al perfil
        s_currentProfile.x = player.transform.position.x;
        s_currentProfile.y = player.transform.position.y;
        //Decimos que el juego no es nuevo
        s_currentProfile.newGame = false;

        //Cogemos los datos del perfil y lo guaurdamos en el archivo correspondiente
        var path = Application.streamingAssetsPath + "/Profiles/" + s_currentProfile.filename;
        SaveFile<ProfileData>(path, s_currentProfile);
    }

    //Función para guardar el archivo del juego
    static void SaveFile<T>(string path, T data)
    {
        var profileWriter = new StreamWriter(path);
        var profileSerializer = new XmlSerializer(typeof(T));
        profileSerializer.Serialize(profileWriter, data);
        profileWriter.Dispose();
    }

    //Función para eliminar el archivo del juego
    //Le pasamos el nombre del archivo que queremos borrar
    public static void DeleteProfile(string filename)
    {
        //Obtenemos su ruta
        var path = Application.streamingAssetsPath + "/Profiles/" + filename;
        //Boramos el archivo
        File.Delete(path);

        //Cogemos el índice
        var index = LoadFile<ProfileIndex>(s_indexpath);
        //Quitamos esee archivo del índice
        index.profileFileNames.Remove(filename);

        //Acualizamos el índice y lo guardamos en su archivo
        SaveFile<ProfileIndex>(s_indexpath, index);
    }

    //Función para cargar el archivo de una partida
    static T LoadFile<T>(string path)
    {
        var profileReader = new StreamReader(path);
        var serializer = new XmlSerializer(typeof(T));
        var obj = (T)serializer.Deserialize(profileReader);
        profileReader.Dispose();

        return obj;
    }
}
