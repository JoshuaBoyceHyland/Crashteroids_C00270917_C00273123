using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NUnit.Framework.Constraints;

public class TestSuite
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
            Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
        game.NewGame();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game.gameObject);
    }

    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        float initialYPos = asteroid.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        Assert.Less(asteroid.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = game.GetShip().transform.position;
        yield return new WaitForSeconds(0.1f);
        Assert.True(game.isGameOver);
    }

    //1
    [Test]
    public void NewGameRestartsGame()
    {
        //2
        game.isGameOver = true;
        game.NewGame();
        //3
        Assert.False(game.isGameOver);
    }

    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        // 1
        GameObject laser = game.GetShip().SpawnLaser();
        // 2
        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator DestroyedAsteroidRaisesScore()
    {
        // 2
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.AreEqual(game.score, 1);
    }

    [UnityTest]
    public IEnumerator ShipMovesLeft()
    {
        // 2
        GameObject ship = game.GetShip().gameObject;
        Vector3 startPosition = ship.transform.position;

        ship.GetComponent<Ship>().MoveLeft();
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.Less(ship.transform.position.x, startPosition.x);
    }

    [UnityTest]
    public IEnumerator ShipStaysInLimits()
    {
        GameObject ship = game.GetShip().gameObject;
        Vector3 startPosition = ship.transform.position + new Vector3(50 , 0 , 0);

        ship.GetComponent<Ship>().MoveLeft();
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.LessOrEqual(ship.transform.position.x, 40.0f);
    }

    [UnityTest]
    public IEnumerator SheildSpawns()
    {
       game.GetSpawner().spawnShield();

        yield return new WaitForSeconds(0.2f);

        SheildPickup shield = GameObject.FindAnyObjectByType<SheildPickup>();

        Assert.IsNotNull(shield.gameObject);
    }


    [UnityTest]
    public IEnumerator SheildSpawnsRandom()
    {
        game.GetSpawner().spawnShield();
        game.GetSpawner().spawnShield();

        yield return new WaitForSeconds(0.2f);

        SheildPickup[] shields = GameObject.FindObjectsOfType<SheildPickup>();
        Assert.AreNotEqual(shields[0].transform.position.x, shields[1].transform.position.x);

    }

    [UnityTest]
    public IEnumerator ShieldPickupMovesDown()
    {
        game.GetSpawner().spawnShield();

        GameObject shield = GameObject.FindAnyObjectByType<SheildPickup>().gameObject;
        float initialYPos = shield.transform.position.y;

        yield return new WaitForSeconds(1.0f);

        Assert.Less(shield.transform.position.y, initialYPos);
        
    }


    [UnityTest]
    public IEnumerator SpeedPickupSpawns()
    {
        game.GetSpawner().SpawnSpeedPickup();
        GameObject SpeedPickup = GameObject.FindAnyObjectByType<SpeedPickup>().gameObject;
        yield return new WaitForSeconds(0.1f);
        Assert.IsNotNull(SpeedPickup);
    }


    [UnityTest]
    public IEnumerator SpeedPickuRandom()
    {
        game.GetSpawner().SpawnSpeedPickup();
        game.GetSpawner().SpawnSpeedPickup();
        SpeedPickup[] SpeedPickups = GameObject.FindObjectsOfType<SpeedPickup>();
        yield return new WaitForSeconds(0.1f);
        Assert.AreNotEqual(SpeedPickups[0].transform.position, SpeedPickups[1].transform.position);
    }


    [UnityTest]
    public IEnumerator SpeedPickupMovesDown()
    {
        game.GetSpawner().SpawnSpeedPickup();
        GameObject SpeedPickup = GameObject.FindAnyObjectByType<SpeedPickup>().gameObject;
        float initialYPos = SpeedPickup.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        Assert.Less(SpeedPickup.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator SpeedPickupWareOff()
    {
        GameObject ship = game.GetShip().gameObject;
        ship.GetComponent<Ship>().Speedup();
        yield return new WaitForSeconds(7.0f);
        Assert.AreEqual(5.0f, ship.GetComponent<Ship>().speed);
    }


}