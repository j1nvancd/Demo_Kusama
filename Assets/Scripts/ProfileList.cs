using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileList : MonoBehaviour
{
    //Sitio donde colocaremos los recuadros
    public Transform profilesHolder;
    //El prefab creado anteriormente
    public GameObject profileUIBoxPrefab;

    private void Start()
    {
        {
            //Leemos el índice
            var index = profileStorage.GetProfileIndex();

            //Por cada uno de los nombres de archivo
            foreach (var profileName in index.profileFileNames)
            {
                //Instanciamos una vez el prefab de ProfileBoxUI
                var go = Instantiate(this.profileUIBoxPrefab);
                //Obtenemos el componente ProfileBox
                var uibox = go.GetComponent<ProfileBoxUI>();
                /*Accedemos a los componentes del prefab
                 * En este caso le metemos a la etiqueta del nombre el nombre del perfil del juego
                */
                uibox.nameLabel.text = profileName;

                //Cuando pulsamos el cargar
                uibox.Cargar.onClick.AddListener(() =>
                {
                    //Cogemos el nombre del archivo del juego del profileStorage
                    profileStorage.LoadProfile(profileName);
                    //Cargamos la escena del juego
                    SceneManager.LoadScene("P_game");
                });

                //Cuando pulsamos el eliminar
                uibox.Eliminar.onClick.AddListener(() =>
                {
                    //Destruimos el archivo del perfil de juego seleccionado
                    profileStorage.DeleteProfile(profileName);
                    //Destruimos ese GameObject
                    Destroy(go);
                });
                //El padre del cuadro que se colocar es el holder
                go.transform.SetParent(this.profilesHolder, false);
            }
        }
    }
}
