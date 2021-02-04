using UnityEngine;
using System.Collections;

public class GiraObjeto : MonoBehaviour {

	void Update () {//chama cada flame
		transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
        //vetor da rotaçao / Time.deltaTime normaliza o tempo da rotaçao com o aparelhos diferentes
    }

}
