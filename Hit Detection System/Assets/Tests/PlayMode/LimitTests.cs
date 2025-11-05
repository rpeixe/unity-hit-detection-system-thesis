using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class LimitTests
{
    [UnityTest]
    public IEnumerator TestHighDefenseVsHighAttackHitThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.49f);
        SetPosition(fighter2, 0.3f);

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
    public IEnumerator TestHighDefenseVsHighAttackMissThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.5f);
        SetPosition(fighter2, 0.3f);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();

        inputController1.PressAttack();
        yield return null;
        inputController1.ReleaseAttack();

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 0, "No hits should be registered.");
    }

    [UnityTest]
    public IEnumerator TestLowDefenseVsHighAttackHitThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.37f);
        SetPosition(fighter2, 0.3f);

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
    public IEnumerator TestLowDefenseVsHighAttackMissThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.38f);
        SetPosition(fighter2, 0.3f);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();
        FighterInputController inputController2 = fighter2.GetComponent<FighterInputController>();

        inputController1.PressAttack();
        inputController2.LeftStick(Vector2.down);
        yield return null;
        inputController1.ReleaseAttack();
        inputController2.LeftStick(Vector2.down);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 0, "No hits should be registered.");
    }

    [UnityTest]
    public IEnumerator TestHighDefenseVsLowAttackHitThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.45f);
        SetPosition(fighter2, 0.3f);

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
    public IEnumerator TestHighDefenseVsLowAttackCriticalHitThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.26f);
        SetPosition(fighter2, 0.3f);

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
    public IEnumerator TestHighDefenseVsLowAttackNonCriticalHitThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.25f);
        SetPosition(fighter2, 0.3f);

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
        Assert.IsFalse(HitRegisterer.Instance.LastHitDecisionOnPlayer2.Critical, "Player 2 should not receive a critical hit.");
    }

    [UnityTest]
    public IEnumerator TestHighDefenseVsLowAttackMissThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.46f);
        SetPosition(fighter2, 0.3f);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();

        inputController1.LeftStick(Vector2.down);
        yield return null;
        inputController1.LeftStick(Vector2.down);
        inputController1.PressAttack();
        yield return null;
        inputController1.ReleaseAttack();

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 0, "No hits should be registered.");
    }

    [UnityTest]
    public IEnumerator TestLowDefenseVsLowAttackHitThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.48f);
        SetPosition(fighter2, 0.3f);

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
    public IEnumerator TestLowDefenseVsLowAttackMissThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.49f);
        SetPosition(fighter2, 0.3f);

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

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 0, "No hits should be registered.");
    }

    [UnityTest]
    public IEnumerator TestHighDefenseVsAirAttackHitThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.24f);
        SetPosition(fighter2, 0.3f);

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
    public IEnumerator TestHighDefenseVsAirAttackMissThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.25f);
        SetPosition(fighter2, 0.3f);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();

        inputController1.PressJump();
        yield return null;
        inputController1.ReleaseJump();

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 0, "No hits should be registered.");
    }

    [UnityTest]
    public IEnumerator TestLowDefenseVsAirAttackHitThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.28f);
        SetPosition(fighter2, 0.3f);

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
    public IEnumerator TestLowDefenseVsAirAttackMissThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.29f);
        SetPosition(fighter2, 0.3f);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();
        FighterInputController inputController2 = fighter2.GetComponent<FighterInputController>();

        inputController1.PressJump();
        inputController2.LeftStick(Vector2.down);
        yield return null;
        inputController1.ReleaseJump();
        inputController2.LeftStick(Vector2.down);

        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 0, "No hits should be registered.");
    }

    [UnityTest]
    public IEnumerator TestAdvancingAirAttackAgainstStillOpponentHitsOnlyOnce()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -1.5f);
        SetPosition(fighter2, 0.3f);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();
        FighterInputController inputController2 = fighter2.GetComponent<FighterInputController>();

        inputController1.LeftStick(Vector2.right);
        yield return null;
        inputController1.PressJump();
        yield return null;
        inputController1.ReleaseJump();


        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 1, "Only 1 hit should be registered.");
    }

    [UnityTest]
    public IEnumerator TestAirAttackAgainstAdvancingOpponentHitsOnlyOnce()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -1f);
        SetPosition(fighter2, 0.3f);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();
        FighterInputController inputController2 = fighter2.GetComponent<FighterInputController>();

        inputController2.LeftStick(Vector2.left);
        inputController1.PressJump();
        yield return null;
        inputController1.ReleaseJump();


        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 1, "Only 1 hit should be registered.");
    }

    [UnityTest]
    public IEnumerator TestAdvancingAirAttackAgainstAdvancingOpponentHitsOnlyOnce()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -1.5f);
        SetPosition(fighter2, 0.3f);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();
        FighterInputController inputController2 = fighter2.GetComponent<FighterInputController>();

        inputController1.LeftStick(Vector2.right);
        inputController2.LeftStick(Vector2.left);
        yield return null;
        inputController1.PressJump();
        yield return null;
        inputController1.ReleaseJump();


        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 1, "Only 1 hit should be registered.");
    }

    [UnityTest]
    public IEnumerator TestAttackCounterHitThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.7f);
        SetPosition(fighter2, 0.3f);

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
    public IEnumerator TestAttackCounterMissThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.71f);
        SetPosition(fighter2, 0.3f);

        DisableControllerInput(fighter1, fighter2);

        FighterInputController inputController1 = fighter1.GetComponent<FighterInputController>();
        FighterInputController inputController2 = fighter2.GetComponent<FighterInputController>();

        inputController1.PressAttack();
        inputController2.PressAttack();
        yield return null;
        inputController1.ReleaseAttack();
        inputController2.ReleaseAttack();


        yield return new WaitForSeconds(1f);

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 0, "No hits should be registered.");
    }

    [UnityTest]
    public IEnumerator TestAntiAirAttackHitThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.45f);
        SetPosition(fighter2, 0.3f);

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

    [UnityTest]
    public IEnumerator TestAntiAirAttackMissThreshold()
    {
        yield return SceneManager.LoadSceneAsync("FightingGamePrototype");
        yield return null;

        GameObject fighter1 = GameObject.Find("Fighter");
        GameObject fighter2 = GameObject.Find("Fighter 2");

        SetPosition(fighter1, -0.46f);
        SetPosition(fighter2, 0.3f);

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

        Assert.IsTrue(HitRegisterer.Instance.NumberOfHitsOnPlayer2 == 0, "No hits should be registered.");
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
