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

    //[UnityTest]
    //public IEnumerator SheildSpawns()
    //{
    //    GameObject shield = game.GetShieldPickup().gameObject;
    //    yield return new WaitForSeconds(0.1f);
    //    Assert.IsNotNull(shield);
    //}


    //[UnityTest]
    //public IEnumerator SheildSpawnsRandom()
    //{
    //    GameObject shield1 = game.GetShieldPickup().gameObject;
    //    GameObject shield2 = game.GetShieldPickup().gameObject;
    //    yield return new WaitForSeconds(0.1f);
    //    Assert.AreNotEqual(shield1.transform.position, shield2.transform.position);
    //}

    //[UnityTest]
    //public IEnumerator ShieldPickupCollision()
    //{
    //    GameObject ship = game.GetShip().gameObject;
    //    GameObject shield = game.GetShieldPickup().gameObject;
    //    ship.transform.position = shield.transform.position;
    //    ship.GetComponent<Ship>().OnCollisionEnter();
    //    yield return new WaitForSeconds(0.1f);
    //    Assert.IsNull(shield.transform.position);
    //    Assert.IsTrue(ship.hasShield);
    //}

    //[UnityTest]
    //public IEnumerator ShieldMovesDown()
    //{
    //    GameObject shield = game.GetShieldPickup().gameObject;
    //    float initialYPos = shield.transform.position.y;
    //    yield return new WaitForSeconds(0.1f);
    //    Assert.Less(shield.transform.position.y, initialYPos);
    //}

    //[UnityTest]
    //public IEnumerator ShieldCollision()
    //{
    //    GameObject shield = game.GetShield().gameObject;
    //    GameObject asteroid = game.GetSpawner().SpawnAsteroid();
    //    GameObject ship = game.GetShip().gameObject;
    //    asteroid.transform.position = game.GetShield().transform.position;
    //    yield return new WaitForSeconds(0.1f);
    //    Assert.IsFalse(ship.hasShield);
    //}

    [UnityTest]
    public IEnumerator StartingScore()
    {
        game.NewGame();
        yield return new WaitForSeconds(0.1f);
        Assert.LessOrEqual(game.score, 0);
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

    //[UnityTest]
    //public IEnumerator SpeedPickupCollision()
    //{
    //    game.GetSpawner().SpawnSpeedPickup();
    //    GameObject SpeedPickup = GameObject.FindAnyObjectByType<SpeedPickup>().gameObject;
    //    GameObject ship = game.GetShip().gameObject;
    //    SpeedPickup.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y - 2, ship.transform.position.z);
    //    yield return new WaitForSeconds(5.4f);
    //    Assert.IsNull(SpeedPickup);
    //}

    [UnityTest]
    public IEnumerator SpeedPickupWareOff()
    {
        GameObject ship = game.GetShip().gameObject;
        ship.GetComponent<Ship>().Speedup();
        yield return new WaitForSeconds(7.0f);
        Assert.AreEqual(5.0f, ship.GetComponent<Ship>().speed);
    }


}