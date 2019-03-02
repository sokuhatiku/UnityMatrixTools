using UnityEngine;

internal interface IMatrixSource
{
	Matrix4x4 Matrix { get; }
	Matrix4x4Event OnUpdateMatrix { get; }
}
