using UnityEngine;

public class ModifyCube : MonoBehaviour {

	private int variablePrivee = 0; //pas utilisé, juste un exemple de variable privée
	int variablePrivee2 = 0; //pas utilisé, juste un exemple de variable privée

	public float height = 1; // variable publique modifiable dans l'inspecteur pendant l'exécution
	public GameObject otherGameObject;

	private Renderer rend;


	// Use this for initialization
	void Start () {

		rend = GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.R))
		{
			rend.material.color = Color.red;
			
			otherGameObject.GetComponent<Renderer> ().material.color = Color.red;
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			rend.material.color = Color.green;
			
		}
		if (Input.GetKeyDown(KeyCode.B))
		{
			rend.material.color = Color.blue;
			
		}
			

		Debug.Log("Update time interval :" + Time.deltaTime);

	}

	void FixedUpdate ()
	{

		GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, height, GetComponent<Transform>().position.z);
		height = height + (float)0.01;


		Debug.Log("FixedUpdate time interval :" + Time.deltaTime);
	}



}
