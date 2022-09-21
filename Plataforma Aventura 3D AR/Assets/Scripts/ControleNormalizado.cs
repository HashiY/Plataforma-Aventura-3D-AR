using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput; // muda o input para  CrossPlatformInputManager

public class ControleNormalizado : MonoBehaviour {

	public GameObject peninhas;

	CharacterController objetoCharControler;
	Transform transformCamera;

	Vector3 moveCameraFrente;
	Vector3 moveMove;
	Vector3 normalZeroPiso = new Vector3(0,0,0); // refe rencia do piso
	Vector3 vetorDirecao = new Vector3(0,0,0); //direçao da gravidade para baixo

    public GameObject jogador;
	public Animation animacao;   

	float giro = 3.0f;
	float frente = 3.0f;
	float velocidade = 5.0f;
	float pulo = 5.0f;

	int numeroObjetos;

    public AudioClip somPula;

    void Start () 
	{ 
		objetoCharControler = GetComponent<CharacterController>();  //pega
        animacao = jogador.GetComponent<Animation>();  //pega
        transformCamera = Camera.main.transform; //recebe a camera com tag mainCamera
        Camera.main.gameObject.SetActive(false);
		objetoCharControler.material.bounciness = 0.0f;
	}

	void Update ()
	{
		moveCameraFrente = Vector3.Scale(transformCamera.forward, new Vector3(1, 0, 1)).normalized;//rescalona o vetor , frentedaCamera,novoVetor,escaladessenovoVetor

		//MobileController();	//--------MOBILE------
		PCController();         //---------PC---------
	}

	void MobileController()
    {
		moveMove = CrossPlatformInputManager.GetAxis("Vertical") * moveCameraFrente + CrossPlatformInputManager.GetAxis("Horizontal") * transformCamera.right;//eixoXdaCamera
		PlayerMove();
		
		if (CrossPlatformInputManager.GetButton("Jump"))//acrescentei o input
		{
			if (objetoCharControler.isGrounded == true)
			{
				PlayerAnimeJump();
			}
		}
		else
		{
			if ((CrossPlatformInputManager.GetAxis("Horizontal") != 0.0f) || (CrossPlatformInputManager.GetAxis("Vertical") != 0.0f))//se ta apertado para cima ou baixo o significado do  != 0.0f
			{
				if (!animacao.IsPlaying("jump"))
				{
					jogador.GetComponent<Animation>().Play("walk");
				}
			}
			else
			{
				if (objetoCharControler.isGrounded == true)
				{
					jogador.GetComponent<Animation>().Play("idle");
				}
			}
		}
	}

	void PCController()
    {
		moveMove = Input.GetAxis("Vertical") * moveCameraFrente + Input.GetAxis("Horizontal") * transformCamera.right;//eixoXdaCamera
		PlayerMove();
		
		if (Input.GetButton("Jump"))
		{
			if (objetoCharControler.isGrounded == true) 
			{
				PlayerAnimeJump();
			}
		}
        else
        {
			if ((Input.GetAxis("Horizontal") != 0.0f) || (Input.GetAxis("Vertical") != 0.0f))//se ta apertado para cima ou baixo o significado do  != 0.0f
			{
				if (!animacao.IsPlaying("jump"))
				{
					jogador.GetComponent<Animation>().Play("walk");
				}
			}
			else
			{
				if (objetoCharControler.isGrounded == true)
				{
					jogador.GetComponent<Animation>().Play("idle");
				}
			}
		}

		if (Input.GetKey(KeyCode.LeftShift)) { velocidade = 2.5f; } else { velocidade = 5; }

	}

	void PlayerMove()
    {
		vetorDirecao.y -= 5.0f * Time.deltaTime;    //inclementar y para baixo
		objetoCharControler.Move(vetorDirecao * Time.deltaTime); // reposiciona o personagem 
		objetoCharControler.Move(moveMove * velocidade * Time.deltaTime);

		if (moveMove.magnitude > 1f) moveMove.Normalize();
		moveMove = transform.InverseTransformDirection(moveMove);

		moveMove = Vector3.ProjectOnPlane(moveMove, normalZeroPiso);
		giro = Mathf.Atan2(moveMove.x, moveMove.z);
		frente = moveMove.z;

		objetoCharControler.SimpleMove(Physics.gravity); // para ter a gravidade
		aplicaRotacao();
	}

	void PlayerAnimeJump()
    {
		vetorDirecao.y = pulo;
		jogador.GetComponent<Animation>().Play("jump");
		GetComponent<AudioSource>().PlayOneShot(somPula, 0.7f);
		//Instantiate(peninhas, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y+1, this.gameObject.transform.position.z), Quaternion.identity);

		GameObject particula = Instantiate(peninhas);
		particula.transform.position = this.transform.position;
	}

	void aplicaRotacao()
	{
		float turnSpeed = Mathf.Lerp(180, 360, frente);
		transform.Rotate(0, giro * turnSpeed * Time.deltaTime, 0);
	}
}
