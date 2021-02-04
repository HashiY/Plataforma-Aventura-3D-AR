using UnityEngine;
using System.Collections;

public class MudaCameraIntro : MonoBehaviour {

	public GameObject cameraPersonagem;
 //criado para fazer a animaçao de introduçao da camera 
 //quando acaba vai para a outra camera
	public void MudaCamera(){

		this.gameObject.SetActive(false); 
		cameraPersonagem.SetActive(true); 

	}
}
