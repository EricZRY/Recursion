using UnityEngine;
using System.Collections;

public class fractal : MonoBehaviour {
	public Mesh mesh;
	public Material material;
	public int maxDepth;
	public float childScale;
	private bool draw=true;
	private int depth;

	private void Start () {
		gameObject.AddComponent<MeshFilter>().mesh = mesh;
		gameObject.AddComponent<MeshRenderer>().material = material;

	}


	private void Update(){

		if(draw==true){
			if (depth < maxDepth) {
				new GameObject("Fractal Child"+depth).AddComponent<fractal>().
					Initialize(this, Vector3.up, Quaternion.identity);
				new GameObject("Fractal Child"+depth).AddComponent<fractal>().
					Initialize(this, Vector3.right, Quaternion.Euler(0f, 0f, -90f));	
				new GameObject("Fractal Child"+depth).AddComponent<fractal>().
					Initialize(this, Vector3.left, Quaternion.Euler(0f, 0f, 90f));
				//			new GameObject("Fractal Child"+depth).AddComponent<fractal>().
				//				Initialize(this, Vector3.back, Quaternion.Euler(90f, 180f, 0f));
				draw=false;
			}
		}
	}


	private void Initialize (fractal parent, Vector3 direction, Quaternion orientation) {
		mesh = parent.mesh;
		material = parent.material;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		childScale = parent.childScale;

		transform.parent = parent.transform;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition = Vector3.up * (0.5f + 0.5f * childScale);
		transform.localPosition = direction * (0.5f + 0.5f * childScale);
		transform.localRotation = orientation;

	}
}
