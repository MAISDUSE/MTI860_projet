using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGlobalAxes : MonoBehaviour {

	public float size = 100f;
	public GameObject poisson;



	// Use this for initialization
	void Start () {

		ApplyTransformations ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(Vector3.right * size, Vector3.zero);

		Gizmos.color = Color.green;
		Gizmos.DrawLine(Vector3.up * size, Vector3.zero);

		Gizmos.color = Color.blue;
		Gizmos.DrawLine(Vector3.forward * size, Vector3.zero);
		Gizmos.color = Color.white;

	}
		

	void ApplyTransformations ()
	{

		// Translation de (0,0,3) : 0m en x, 0m en y , 3m en z dans le système de coordonnées local (par défaut)
		//poisson.transform.Translate (new Vector3 (0, 0, 3));


		// Translation de (0,0,3) : 0m en x, 0m en y , 3m en z dans le système de coordonnées global 
		poisson.transform.Translate(new Vector3(0, 0, 3), Space.World);

		// Rotation autours de l'axe z du système de coordonnées globale
		//poisson.transform.RotateAround (new Vector3 (0, 0, 0), new Vector3 (0, 0, 1), 60);

		// Rotation du poisson sur lui même, autours de son z
		//poisson.transform.Rotate(0,0,60, Space.Self);

		// Rotation du poisson sur lui même, autours de l'axe z global
		//poisson.transform.Rotate(0,0,60, Space.World);

		//Mise à l'échelle du poisson
		//poisson.transform.localScale += new Vector3(30,30,30);


	}

}
