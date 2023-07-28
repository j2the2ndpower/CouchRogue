using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Instruction : MonoBehaviour {
    protected Map map;

    public void SetMap(Map map) {
        this.map = map;
    }

    abstract public void Perform();
}
