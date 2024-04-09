using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EscaleraTesting
{
    // A Test behaves as an ordinary method
    [Test]
    public void EscaleraTestingSimplePasses()
    {
        // Use the Assert class to test conditions
        
    }

    /*
     * Test para comprobar que al iniciar el juego, la escalera no tiene ningun objeto cerca que altere los estados
     * para comprobar si puede subir el jugador
     */
    [Test]
    public void ComprobarEstadoInicialTest()
    {
        Escalera escalera = new Escalera();
        Assert.IsFalse(escalera.ComprobarEstadoInicial());
    }

    /*
     * Test para comprobar que al iniciar el juego, el jugador no tenga ningun objeto vinculado
     */
    [Test]
    public void ComprobarObjetoInicialTest()
    {
        Player player = new Player();
        Assert.IsTrue(player.ObjetoInicial());
    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator EscaleraTestingWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
