using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokuhatiku.MatrixTools
{
	public class MatrixEditor : MonoBehaviour, IMatrixSource
	{
#pragma warning disable 0414
		[SerializeField, Multiline]
		private string note = "";
		[SerializeField]
		private bool enableGizmo = false;
#pragma warning restore 0414

		[SerializeField, MatrixField]
		private Matrix4x4 matrix = Matrix4x4.identity;
		public Matrix4x4 Matrix
		{ get { return matrix; } }

		[SerializeField]
		Matrix4x4Event updateMatrixEvent = new Matrix4x4Event();
		public Matrix4x4Event OnUpdateMatrix
		{ get { return updateMatrixEvent; } }

		private Mesh meshCache = null;

		private void OnValidate()
		{
			if (meshCache != null)
			{
				if (Application.isPlaying)
					Destroy(meshCache);
				else
					DestroyImmediate(meshCache);
				meshCache = null;
			}
		}

		public void UpdateMatrix(Matrix4x4 matrix)
		{
			this.matrix = matrix;
			updateMatrixEvent.Invoke(matrix);
		}

		public void SetPointsSource(Vector3[] points)
		{
			throw new System.NotImplementedException();
		}

		public void SetFrustumSource(float left, float right, float bottom, float top, float zNear, float zFar)
		{
			var matrix = Matrix4x4.Frustum(left, right, bottom, top, zNear, zFar);
			UpdateMatrix(matrix);
		}

		public void SetTransformSource(Vector3 tlanslation, Quaternion rotation, Vector3 scale)
		{
			var matrix = Matrix4x4.TRS(tlanslation, rotation, scale);
			UpdateMatrix(matrix);
		}

		private void OnDrawGizmos()
		{
			if (!enableGizmo)
				return;

			if (meshCache == null)
				meshCache = GenerateMatrixMesh(matrix);

			var tf = transform;
			Gizmos.DrawWireMesh(meshCache, tf.position, tf.rotation, tf.lossyScale);
		}

		private static Mesh GenerateMatrixMesh(Matrix4x4 matrix)
		{
			var mesh = GenerateCubeMesh();
			var verts = mesh.vertices;
			for (int i = 0; i < verts.Length; i++)
			{
				verts[i] = matrix.MultiplyPoint(verts[i]);
			}
			mesh.vertices = verts;
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			return mesh;
		}

		private static Mesh GenerateCubeMesh()
		{
			var mesh = new Mesh();
			var vertices = new Vector3[] {
			new Vector3(-1, -1, -1),
			new Vector3(-1, +1, -1),
			new Vector3(+1, -1, -1),
			new Vector3(+1, +1, -1),
			new Vector3(-1, -1, +1),
			new Vector3(-1, +1, +1),
			new Vector3(+1, -1, +1),
			new Vector3(+1, +1, +1),
		};
			var qpi = 0.70710678118f;
			var normals = new Vector3[]
			{
			new Vector3(-qpi, -qpi, -qpi),
			new Vector3(+qpi, -qpi, -qpi),
			new Vector3(-qpi, +qpi, -qpi),
			new Vector3(+qpi, +qpi, -qpi),
			new Vector3(-qpi, -qpi, +qpi),
			new Vector3(+qpi, -qpi, +qpi),
			new Vector3(-qpi, +qpi, +qpi),
			new Vector3(+qpi, +qpi, +qpi),
			};

			var indices = new int[]
			{
			// 前
			0, 1, 2,
			2, 1, 3,

			// 後
			5, 4, 7,
			7, 4, 6,

			// 下
			1, 0, 5,
			5, 0, 4,

			// 上
			2, 3, 6,
			6, 3, 7,

			// 左
			4, 0, 6,
			6, 0, 2,

			// 右
			1, 5, 3,
			3, 5, 7
			};

			mesh.vertices = vertices;
			mesh.normals = normals;
			mesh.SetIndices(indices, MeshTopology.Triangles, 0);

			return mesh;
		}

	}
}