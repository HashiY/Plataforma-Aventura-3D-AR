using UnityEngine;
using System.Collections;

public class PegaItem : MonoBehaviour {
	GameObject objetoPrincipal;
 

	public Color corParticulas;
	public GameObject particula;

	void Start () {
		objetoPrincipal = GameObject.Find("GameEngine");  //mandar mensagem para esse nome
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.tag == "Player") // se o tag e 
		{   
			

			switch (gameObject.tag){ // qual tag vai pegar
			case "Ovo": objetoPrincipal.SendMessage("PegaOvo"); break; // realiza a funçao do script principal
			case "Pena": objetoPrincipal.SendMessage("PegaPena"); break;
			case "Estrela": objetoPrincipal.SendMessage("PegaEstrela"); break;
			case "Fogo": objetoPrincipal.SendMessage("EfeitoDePancada"); break;
			case "Finish": objetoPrincipal.SendMessage("CaiuNoBuraco"); break;
				default: break;
			}

			if(particula != null){ // se nao for nula
				GameObject minhaParticula = Instantiate(particula);
				minhaParticula.transform.position = this.transform.position; 

				minhaParticula.GetComponent<ParticleSystem>().startColor = corParticulas;
				Destroy(this.gameObject); 
			}
			  
		} 
	}


}
