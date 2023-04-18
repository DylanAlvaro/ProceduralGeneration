using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GenerateQuad : MonoBehaviour
{

	private MeshFilter meshFilter;
	private new MeshRenderer meshRenderer;
	
    // Start is called before the first frame update
   private void Start()
   {
	   meshFilter = gameObject.GetComponent<MeshFilter>();
	   meshRenderer = gameObject.GetComponent<MeshRenderer>();

	   Vector3[] verts = 
	   {
		   Vector3.up,
		   Vector3.up + Vector3.right,
		   Vector3.zero,
		   Vector3.right,
	   };

	   int[] tris = 
	   {
		   0, 1, 3,
		   0, 3, 2
	   };

	   Vector3[] normals =
	   {
		   Vector3.forward,
		   Vector3.forward,
		   Vector3.forward,
		   Vector3.forward,
	   };

	   Vector2[] uvs =
	   {
		   new(0, 0),
		   new(1, 0),
		   new(0, 1),
		   new(1, 1)
	   };

	   Color[] colors =
	   {
		   Color.green,
		   Color.red,
		   Color.blue,
		   Color.yellow
	   };

	   Mesh mesh = new()
	   {
		   vertices = verts.Cast<Vector3>().ToArray(),
		   uv = uvs.Cast<Vector2>().ToArray(),
		   normals = normals.Cast<Vector3>().ToArray(),
		   triangles = tris,
		   colors = colors
	   };
	   
	   mesh.RecalculateBounds();
	   mesh.RecalculateTangents();
	   meshFilter.mesh = mesh;

	   Texture2D texture2D = new(64, 64, TextureFormat.RGBA32, false)
	   {
		   filterMode = FilterMode.Point,
		   alphaIsTransparency = true
	   };

	   bool isGray = true;
	   for(int x = 0; x < texture2D.width; x++)
	   {
		   for(int y = 0; y < texture2D.height; y++)
		   {
			   if((y * texture2D.width + x) % 4 == 0)
			   {
				   isGray = !isGray;

				   Color color = isGray ? Color.gray : Color.red;
				   texture2D.SetPixel(x, y, color);
			   }
		   }
	   }
	   texture2D.Apply();
	   meshRenderer.material.mainTexture = texture2D;
   }
}
