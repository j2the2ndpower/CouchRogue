using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public Transform clearness;
    public Transform darkness;
    private float revealTime = 0.5f;
    private float currentTime = 0f;
    private bool revealing = false;

    public GameObject darkTopLeft;
    public GameObject darkEdgeLeft;


    void Start() {
        clearness.gameObject.SetActive(false);
        CreateEdges();
    }

    void Update() {
        if (revealing) {
            currentTime += Time.deltaTime;
            float endSize = Mathf.Max(darkness.localScale.x, darkness.localScale.y)*2.5f;
            float clearnessSize = Mathf.Lerp(1, endSize, currentTime/revealTime);
            clearness.localScale = new Vector3(clearnessSize, clearnessSize, 1);
            if (currentTime >= revealTime) {
                revealing = false;
                Destroy(gameObject);
            }
        }
    }

    public void RevealRoom(Transform revealer) {
        if (!revealing) {
            clearness.position = revealer.position;
            clearness.gameObject.SetActive(true);
            revealing = true;

            foreach(SpriteRenderer sr in gameObject.GetComponentsInChildren<SpriteRenderer>()) {
                if (sr.sortingOrder == 7) 
                    sr.sortingOrder = 1;
            }
        }
    }

    public void CreateEdges() {
        GameObject tl = GameObject.Instantiate(darkTopLeft, transform.position, transform.rotation, transform);
        tl.transform.localPosition = darkness.localPosition + new Vector3(-darkness.localScale.x / 2 - 0.5f, darkness.localScale.y / 2 + 0.5f, 0f);

        GameObject tr = GameObject.Instantiate(darkTopLeft, transform.position, transform.rotation, transform);
        tr.transform.Rotate(new Vector3(0f, 0f, -90f));
        tr.transform.localPosition = darkness.localPosition + new Vector3(darkness.localScale.x / 2 + 0.5f, darkness.localScale.y / 2 + 0.5f, 0f);

        GameObject bl = GameObject.Instantiate(darkTopLeft, transform.position, transform.rotation, transform);
        bl.transform.Rotate(new Vector3(0f, 0f, 180f));
        bl.transform.localPosition = darkness.localPosition + new Vector3(darkness.localScale.x / 2 + 0.5f, -darkness.localScale.y / 2 - 0.5f, 0f);

        GameObject br = GameObject.Instantiate(darkTopLeft, transform.position, transform.rotation, transform);
        br.transform.Rotate(new Vector3(0f, 0f, 90));
        br.transform.localPosition = darkness.localPosition + new Vector3(-darkness.localScale.x / 2 - 0.5f, -darkness.localScale.y / 2 - 0.5f, 0f);

        GameObject el = GameObject.Instantiate(darkEdgeLeft, transform.position, transform.rotation, transform);
        el.transform.localPosition = darkness.localPosition + new Vector3(-darkness.localScale.x / 2 - 0.5f, 0f, 0f);
        el.transform.localScale = new Vector3(1f, darkness.localScale.y, 1f);

        GameObject et = GameObject.Instantiate(darkEdgeLeft, transform.position, transform.rotation, transform);
        et.transform.Rotate(new Vector3(0f, 0f, -90));
        et.transform.localPosition = darkness.localPosition + new Vector3(0f, darkness.localScale.y / 2 + 0.5f, 0f);
        et.transform.localScale = new Vector3(1f, darkness.localScale.y, 1f);

        GameObject er = GameObject.Instantiate(darkEdgeLeft, transform.position, transform.rotation, transform);
        er.transform.Rotate(new Vector3(0f, 0f, 180));
        er.transform.localPosition = darkness.localPosition + new Vector3(darkness.localScale.x / 2 + 0.5f, 0f, 0f);
        er.transform.localScale = new Vector3(1f, darkness.localScale.y, 1f);

        GameObject eb = GameObject.Instantiate(darkEdgeLeft, transform.position, transform.rotation, transform);
        eb.transform.Rotate(new Vector3(0f, 0f, 90));
        eb.transform.localPosition = darkness.localPosition + new Vector3(0f, -darkness.localScale.y / 2 - 0.5f, 0f);
        eb.transform.localScale = new Vector3(1f, darkness.localScale.x, 1f);
    }
}
