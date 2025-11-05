using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class UseCasesTests
{
    [UnityTest]
    public IEnumerator TestHighDefenseBlocksHighAttack()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetStartingPositionClose(fighter1, fighter2);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();

        inputController1.PressAttack();
        yield return null;
        inputController1.ReleaseAttack();

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 1, "Only 1 hit should be registered.");

        Assert.IsTrue(HitRegisterer.Instance.LastHitDecisionOnPlayer2.Blocked, "Player 2 should block.");
    }

    [UnityTest]
    public IEnumerator TestLowDefenseDoesNotBlockHighAttack()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetStartingPositionClose(fighter1, fighter2);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();
        FighterInputController inputController2 = fighter2.GetComponent<FighterInputController>();

        inputController1.PressAttack();
        inputController2.LeftStick(Vector2.down);
        yield return null;
        inputController1.ReleaseAttack();
        inputController2.LeftStick(Vector2.down);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 1, "Only 1 hit should be registered.");

        Assert.IsFalse(HitRegisterer.Instance.LastHitDecisionOnPlayer2.Blocked, "Player 2 should not block.");
    }

    [UnityTest]
    public IEnumerator TestHighDefenseDoesNotBlockLowAttack()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetStartingPositionClose(fighter1, fighter2);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();

        inputController1.LeftStick(Vector2.down);
        yield return null;
        inputController1.LeftStick(Vector2.down);
        inputController1.PressAttack();
        yield return null;
        inputController1.ReleaseAttack();

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 1, "Only 1 hit should be registered.");

        Assert.IsFalse(HitRegisterer.Instance.LastHitDecisionOnPlayer2.Blocked, "Player 2 should not block.");
    }

    [UnityTest]
    public IEnumerator TestLowDefenseBlocksLowAttack()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetStartingPositionClose(fighter1, fighter2);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();
        FighterInputController inputController2 = fighter2.GetComponent<FighterInputController>();

        inputController1.LeftStick(Vector2.down);
        inputController2.LeftStick(Vector2.down);
        yield return null;
        inputController1.LeftStick(Vector2.down);
        inputController1.PressAttack();
        inputController2.LeftStick(Vector2.down);
        yield return null;
        inputController1.ReleaseAttack();

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 1, "Only 1 hit should be registered.");

        Assert.IsTrue(HitRegisterer.Instance.LastHitDecisionOnPlayer2.Blocked, "Player 2 should block.");
    }

    [UnityTest]
    public IEnumerator TestLowAttackCriticalHitsWhenFar()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetStartingPositionMedium(fighter1, fighter2);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();

        inputController1.LeftStick(Vector2.down);
        yield return null;
        inputController1.LeftStick(Vector2.down);
        inputController1.PressAttack();
        yield return null;
        inputController1.ReleaseAttack();

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 1, "Only 1 hit should be registered.");

        Assert.IsFalse(HitRegisterer.Instance.LastHitDecisionOnPlayer2.Blocked, "Player 2 should not block.");
        Assert.IsTrue(HitRegisterer.Instance.LastHitDecisionOnPlayer2.Critical, "Player 2 should receive a critical hit.");
    }

    [UnityTest]
    public IEnumerator TestHighDefenseBlocksAirAttack()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetStartingPositionClose(fighter1, fighter2);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();

        inputController1.PressJump();
        yield return null;
        inputController1.ReleaseJump();

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 1, "Only 1 hit should be registered.");

        Assert.IsTrue(HitRegisterer.Instance.LastHitDecisionOnPlayer2.Blocked, "Player 2 should block.");
    }

    [UnityTest]
    public IEnumerator TestLowDefenseDoesNotBlockAirAttack()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetStartingPositionClose(fighter1, fighter2);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();
        FighterInputController inputController2 = fighter2.GetComponent<FighterInputController>();

        inputController1.PressJump();
        inputController2.LeftStick(Vector2.down);
        yield return null;
        inputController1.ReleaseJump();
        inputController2.LeftStick(Vector2.down);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 1, "Only 1 hit should be registered.");

        Assert.IsFalse(HitRegisterer.Instance.LastHitDecisionOnPlayer2.Blocked, "Player 2 should not block.");
    }

    [UnityTest]
    public IEnumerator TestAttackCounterHitsAttackingOpponent()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetStartingPositionClose(fighter1, fighter2);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();
        FighterInputController inputController2 = fighter2.GetComponent<FighterInputController>();

        inputController1.PressAttack();
        inputController2.PressAttack();
        yield return null;
        inputController1.ReleaseAttack();
        inputController2.ReleaseAttack();


        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 1, "Only 1 hit should be registered.");

        Assert.IsFalse(HitRegisterer.Instance.LastHitDecisionOnPlayer2.Blocked, "Player 2 should not block.");
        Assert.IsTrue(HitRegisterer.Instance.LastHitDecisionOnPlayer2.Counter, "Player 2 should receive a counter hit.");
    }

    [UnityTest]
    public IEnumerator TestAntiAirAttackIgnoresAirAttack()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetStartingPositionMedium(fighter1, fighter2);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();
        FighterInputController inputController2 = fighter2.GetComponent<FighterInputController>();

        inputController2.PressAttack();
        yield return null;
        inputController2.ReleaseAttack();
        yield return null;
        yield return null;
        inputController1.PressJump();


        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 1, "Only 1 hit should be registered.");

        Assert.IsFalse(HitRegisterer.Instance.LastHitDecisionOnPlayer2.Blocked, "Player 2 should not block.");
        Assert.IsTrue(HitRegisterer.Instance.LastHitDecisionOnPlayer2.Ignored, "Player 2 should ignore the air hit.");
    }

    private void DisableControllerInput(GameObject player1, GameObject player2)
    {
        player1.GetComponent<PlayerInput>().enabled = false;
        player2.GetComponent<PlayerInput>().enabled = false;
    }

    private void SetPosition(GameObject player, float positionX)
    {
        player.transform.position = new Vector3(positionX, -0.9f, 0);
    }

    private void SetStartingPositionClose(GameObject player1, GameObject player2)
    {
        SetPosition(player1, -0.15f);
        SetPosition(player2, 0.15f);
    }

    private void SetStartingPositionMedium(GameObject player1, GameObject player2)
    {
        SetPosition(player1, -0.3f);
        SetPosition(player2, 0.3f);
    }
}
