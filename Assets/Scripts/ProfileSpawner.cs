using UnityEngine;

//Script dedicado a  instanciar el jugador
public class ProfileSpawner : MonoBehaviour
{
    public Transform newGameSpawn;
    public GameObject playerPrefab;

    private void Start()
    {   /*Instancia el prefab del jugador en unas coordenadas especificas para comenzar el juego
         * (profileStorage.s_currentProfile == null ||) se quita en el juego final ya que esto mira que
         * el perfil sea nulo para poder entrar en un perfil sin pasar por la pantalla de carga
         */

        if (profileStorage.s_currentProfile == null || profileStorage.s_currentProfile.newGame)
        {
            Instantiate(this.playerPrefab, this.newGameSpawn.position, Quaternion.identity);
        }
        //Si no es un juego nuevo
        else
        {
            //Cargamos las coordenadas x e y del jugador
            float x = profileStorage.s_currentProfile.x;
            float y = profileStorage.s_currentProfile.y;
            //Cargamos la posición
            Vector3 pos = new Vector3(x, y, 0) ;
            //Instanciamos el jugador en la posición indicada
            Instantiate(this.playerPrefab.transform, pos, Quaternion.identity) ;
        }
    }
}
