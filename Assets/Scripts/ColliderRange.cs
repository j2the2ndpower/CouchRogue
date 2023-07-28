using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class ColliderRange : MonoBehaviour {
    [SerializeField] public int range;
    PolygonCollider2D polyCollider;

    private void OnTriggerStay2D(Collider2D collision) {
        transform.parent.GetComponent<CREnemy>().OnEnterRange(this, collision);
    }
    public void OnTriggerExit2D(Collider2D collision) {
        transform.parent.GetComponent<CREnemy>().OnExitRange(this, collision);
    }

    [ExecuteInEditMode]
    public void OnValidate() {
        polyCollider = GetComponent<PolygonCollider2D>();
        List<Vector2> points = new List<Vector2>();
        float distance;
        bool foundOne = false;

        for (int y = range+1; y >= -range; y--) {
            foundOne = false;
            for (int x = -range; x <= range+1; x++) {
                distance = Util.GridDistance(new Vector3Int(x,y,0));
                if (!foundOne && distance <= range) {
                    points.Insert(0, new Vector2(x, y));
                    points.Insert(0, new Vector2(x, y-1));
                    foundOne = true;
                } else if (foundOne && distance > range) {
                    points.Add(new Vector2(x, y));
                    points.Add(new Vector2(x, y-1));
                    break;
                }
            }
        }

        polyCollider.offset = new Vector2(-0.5f, 0.5f);
        polyCollider.points = points.ToArray();
    }
}
