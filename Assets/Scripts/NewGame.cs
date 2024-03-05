using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Script dedicado a recoger el nombre de la partida y cargar el juego
public class NewGame: MonoBehaviour
{   //Inputfield donde escribiremos el nombre de nuestra partida
    public InputField profileInput;

    public void Generate()
    {   //Recogemos el nombre que hemos dado en el inputfield
        string profileName = this.profileInput.text;
        //Se lo pasamos a la funcion CreateNewGame
        profileStorage.CreateNewGame(profileName);

        //Cargamos la escena del juego
        SceneManager.LoadScene("P_game");
    }
    
    //Método para salir al menú principal
    public void Salir()
    {
        SceneManager.LoadScene("Principal");
    }
}