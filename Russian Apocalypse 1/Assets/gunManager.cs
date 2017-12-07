using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunManager : MonoBehaviour {

    public List<MonoBehaviour> monos;
    public int ind = 0;

    void Start() {
        Equp(0);
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(1)) {
            Next();
        }
	}

    void Equp(int index) {
       
        monos[index].gameObject.SetActive(true);
    }

    void Next() {
        
        monos[ind].gameObject.SetActive(false);
        ind++;
        ind %= monos.Count;
        monos[ind].gameObject.SetActive(true);
    }

    public void AddGun(MonoBehaviour g) {
        g.gameObject.SetActive(false);
        monos.Add(g);
    }
}
