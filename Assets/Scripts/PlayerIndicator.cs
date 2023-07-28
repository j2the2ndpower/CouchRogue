using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIndicator : MonoBehaviour {
    [SerializeField] public SpriteRenderer indicator;
    [SerializeField] public SpriteRenderer direction;

    public void OnInput(InputAction.CallbackContext ctx) {
        Vector2 moveVector = ctx.ReadValue<Vector2>();
        float moveAngle = Util.Angle(moveVector);
        direction.transform.rotation = Quaternion.AngleAxis(-moveAngle, Vector3.forward);
    }

    public void SetColor(Color color) {
        indicator.color = color;
        direction.color = color;
    }

    public void SetPointerActive(bool active) {
        direction.gameObject.SetActive(active);
    }
}
