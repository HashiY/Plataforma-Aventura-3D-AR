using UnityEngine;
using System.Collections;

public class ControleAnimacoes : MonoBehaviour {

	private float velocidade = 1.0f;
	private float giro = 180.0f;
	private float gravidade = 3.5f;
	private float pulo = 6.0f;
	private CharacterController objetoCharControler;
	private Vector3 vetorDirecao = new Vector3(0,0,0); //direçao da gravidade para baixo

    public GameObject jogador;
	public Animation animacao;

	void Start () { 
		objetoCharControler = GetComponent<CharacterController>(); //pega
		animacao = jogador.GetComponent<Animation>(); //pega
	}

	void Update (){ 
		if (Input.GetKey(KeyCode.LeftShift)) { velocidade = 2.5f; } else{velocidade = 5;}

		Vector3 forward = Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * velocidade;// propria direçao * vetor * velociadde
        transform.Rotate(new Vector3(0,Input.GetAxis("Horizontal") * giro *Time.deltaTime,0));// muda de direçao no y / o eixo da rotaçao

        objetoCharControler.Move(forward * Time.deltaTime);// para nao ficar muito rapido(normalizar)
        objetoCharControler.SimpleMove(Physics.gravity); // para ter a gravidade

        if (Input.GetButton("Jump")) // pode ser q nao precisa
		{
			if (objetoCharControler.isGrounded == true) { vetorDirecao.y = pulo; }
		} 

		if(Input.GetButton("Jump"))
		{
			if (objetoCharControler.isGrounded == true) {
				vetorDirecao.y = pulo;
				jogador.GetComponent<Animation>().Play("jump"); // anima
			}
		}else
		{
			if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")  ) // se fazer isso
			{
				if (!animacao.IsPlaying("jump")) // se nao ter a animaçao
				{     
					jogador.GetComponent<Animation>().Play("walk"); // anima
				}

			}else // e se nao fazer nada 
			{
				if (objetoCharControler.isGrounded == true)  // no chao
				{    
					jogador.GetComponent<Animation>().Play("idle"); // anima
				}
			}
		}

		vetorDirecao.y -= gravidade * Time.deltaTime;    // para cair depois do pulo
        objetoCharControler.Move(vetorDirecao * Time.deltaTime);// mover com o vetor de direçao
    }
}
