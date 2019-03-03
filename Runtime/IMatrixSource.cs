using UnityEngine;

namespace Sokuhatiku.MatrixTools
{
	internal interface IMatrixSource
	{
		Matrix4x4 Matrix { get; }
		Matrix4x4Event OnUpdateMatrix { get; }
	}
}