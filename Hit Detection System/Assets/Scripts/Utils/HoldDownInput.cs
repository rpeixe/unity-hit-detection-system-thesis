using System.Collections;
using UnityEngine;

public class HoldDownInput : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(PressDown());
    }

    private IEnumerator PressDown()
    {
        FighterInputController controller = GetComponent<FighterInputController>();

        while (true)
        {
            controller.LeftStick(Vector2.down);
            yield return null;
        }
    }
}
