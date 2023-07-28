using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifyOperation {
    Add,
    Multiply,
    Power,
    Equal,
}

public class Util {
    public static float Angle(Vector2 vector) {
        if (vector.x < 0) {
            return 360 - (Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg * -1);
        } else {
            return Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
        }
    }

    public static float SnapTo(float input, float factor = 90f) {
        return Mathf.Round(input / factor) * factor;
    }

    public static int WeightRandom(int[] weights) {
        int totalWeight = 0;
        for (int x = 0; x < weights.Length; x++) {
            totalWeight += weights[x];
        }
        int rnd = Random.Range(0, totalWeight);
        for (int x = 0; x < weights.Length; x++) {
            rnd -= weights[x];
            if (rnd < 0) {
                return x;
            }
        }
        return -1;
    }

    public static float GridDistance(Vector3Int pos, float diagonalValue = 1.5f) {
        float x = Mathf.Abs(pos.x);
        float y = Mathf.Abs(pos.y);

        return Mathf.Min(x, y) * diagonalValue + (Mathf.Max(x, y) - Mathf.Min(x, y));
    }
    
    public static float ApplyOperator(float value, float value2, ModifyOperation op) {
        if (op == ModifyOperation.Add) {
            return value + value2;
        } else if (op == ModifyOperation.Multiply) {
            return value * value2;
        } else if (op == ModifyOperation.Power) {
            return Mathf.Pow(value, value2);
        } else {
            return value2;
        }
    }

    public static List<CRUnit> GetTargetsOfType(TargetAlignment alignment, List<CRUnit> targets, CRUnit source, float radius = 0f) {
        List<CRUnit> affectedUnits = new List<CRUnit>();

        foreach(CRUnit target in Game.GetUnits()) {
            if ((alignment & TargetAlignment.Affected) > 0 && !targets.Contains(target)) continue;
            if ((alignment & TargetAlignment.NotAffected) > 0 && targets.Contains(target)) continue;
            if ((alignment & TargetAlignment.Friendly) > 0 && !target.IsFriendly(source)) continue;
            if ((alignment & TargetAlignment.Enemy) > 0 && target.IsFriendly(source)) continue;
            if ((alignment & TargetAlignment.Self) > 0 && target != source) continue;
            if ((alignment & TargetAlignment.NotSelf) > 0 && target == source) continue;
            if ((alignment & TargetAlignment.Dead) > 0 && !target.isDead) continue;
            if ((alignment & TargetAlignment.InRadius) > 0) {
                float distance;
                if (source.inCombat) {
                    distance = GridDistance(source.gridPosition - target.gridPosition);
                } else {
                    distance = Vector3.Distance(source.transform.position, target.transform.position);
                    radius += 0.5f;
                }
                if (distance > radius) continue;
            }
            
            affectedUnits.Add(target);
        }

        return affectedUnits;
    }
}
