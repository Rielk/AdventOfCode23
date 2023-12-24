using AdventOfCode23Utilities;
using System.Diagnostics;
using System.Numerics;

namespace AdventOfCode23Day24;
internal class HailStorm
{
	private List<Hailstone> Stones { get; } = [];

	public Vector3 RockPos { get; }
	public Vector3 RockVel { get; }

	public HailStorm(string[] input)
	{
		foreach (string line in input)
			Stones.Add(new(line));

		FindRockIntersect(out Vector3 pos, out Vector3 vel);
		RockPos = pos;
		RockVel = vel;
	}

	private void FindRockIntersect(out Vector3 pos, out Vector3 vel)
	{
		Hailstone stone1 = Stones[0];
		Hailstone stone2 = Stones[1];
		Hailstone stone3 = Stones[2];

		Vector3 pDif1 = stone2.Position - stone1.Position;
		Vector3 pDif2 = stone3.Position - stone1.Position;
		Vector3 vDif1 = stone1.Velocity - stone2.Velocity;
		Vector3 vDif2 = stone1.Velocity - stone3.Velocity;

		float[,] pSkew1 = SkewMatrix(pDif1);
		float[,] pSkew2 = SkewMatrix(pDif2);
		float[,] vSkew1 = SkewMatrix(vDif1);
		float[,] vSkew2 = SkewMatrix(vDif2);

		float[,] stack1 = StackHorizontal(pSkew1, vSkew1);
		float[,] stack2 = StackHorizontal(pSkew2, vSkew2);
		float[,] M = StackVertical(stack1, stack2);
		float[,] invertM = Inverse(M);

		Vector3 rhs1 = Vector3.Cross(stone2.Position, stone2.Velocity) - Vector3.Cross(stone1.Position, stone1.Velocity);
		Vector3 rhs2 = Vector3.Cross(stone3.Position, stone3.Velocity) - Vector3.Cross(stone1.Position, stone1.Velocity);
		float[,] RHS = StackVertical(ToArray(rhs1), ToArray(rhs2));

		float[,] result = MultiplyMatrix(invertM, RHS);
		vel = -new Vector3((float)Math.Round(result[0, 0]), (float)Math.Round(result[0, 1]), (float)Math.Round(result[0, 2]));
		pos = -new Vector3((float)Math.Round(result[0, 3]), (float)Math.Round(result[0, 4]), (float)Math.Round(result[0, 5]));
	}

	private static float[,] MultiplyMatrix(float[,] m1, float[,] m2)
	{
		int r1 = m1.GetLength(1);
		int c1 = m1.GetLength(0);
		int r2 = m2.GetLength(1);
		int c2 = m2.GetLength(0);

		Debug.Assert(c1 == r2);
		float[,] kHasil = new float[c2, r1];

		for (int y = 0; y < r1; y++)
		{
			for (int x2 = 0; x2 < c2; x2++)
			{
				float temp = 0;
				for (int x1 = 0; x1 < c1; x1++)
				{
					temp += m1[x1, y] * m2[x2, x1];
				}
				kHasil[x2, y] = temp;
			}
		}

		return kHasil;
	}

	private static float[,] Inverse(float[,] m)
	{
		int n = m.GetLength(1);
		float[,] result = Duplicate(m);

		float[,] lum = MatrixDecompose(m, out int[] perm, out _) ?? throw new Exception("Unable to compute inverse");
		float[] b = new float[n];
		for (int i = 0; i < n; ++i)
		{
			for (int j = 0; j < n; ++j)
			{
				if (i == perm[j])
					b[j] = 1.0f;
				else
					b[j] = 0.0f;
			}

			float[] x = HelperSolve(lum, b);

			for (int j = 0; j < n; ++j)
				result[i, j] = x[j];
		}
		return result;
	}

	private static float[,] SkewMatrix(Vector3 vector)
	{
		float[,] ret = new float[3, 3];
		(ret[0, 0], ret[0, 1], ret[0, 2]) = (0, -vector.Z, vector.Y);
		(ret[1, 0], ret[1, 1], ret[1, 2]) = (vector.Z, 0, -vector.X);
		(ret[2, 0], ret[2, 1], ret[2, 2]) = (-vector.Y, vector.X, 0);
		return ret;
	}

	private static float[,] ToArray(Vector3 vector)
	{
		float[,] ret = new float[1, 3];
		(ret[0, 0], ret[0, 1], ret[0, 2]) = (vector.X, vector.Y, vector.Z);
		return ret;
	}

	private static float[,] StackHorizontal(float[,] m1, float[,] m2)
	{
		int height = m1.GetLength(1);
		Debug.Assert(height == m2.GetLength(1));
		int length1 = m1.GetLength(0);
		int length2 = m2.GetLength(0);
		float[,] ret = new float[length1 + length2, height];
		for (int j = 0; j < height; j++)
		{
			for (int i = 0; i < length1; i++)
				ret[i, j] = m1[i, j];
			for (int i = 0; i < length2; i++)
				ret[i + length1, j] = m2[i, j];
		}
		return ret;
	}

	private static float[,] StackVertical(float[,] m1, float[,] m2)
	{
		int width = m1.GetLength(0);
		Debug.Assert(width == m2.GetLength(0));
		int length1 = m1.GetLength(1);
		int length2 = m2.GetLength(1);
		float[,] ret = new float[width, length1 + length2];
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < length1; j++)
				ret[i, j] = m1[i, j];
			for (int j = 0; j < length2; j++)
				ret[i, j + length1] = m2[i, j];
		}
		return ret;
	}

	private static float[,] Duplicate(float[,] m)
	{
		float[,] ret = new float[m.GetLength(0), m.GetLength(1)];
		for (int i = 0; i < m.GetLength(0); ++i) // copy the values
			for (int j = 0; j < m.GetLength(1); ++j)
				ret[i, j] = m[i, j];
		return ret;
	}

	private static float[] HelperSolve(float[,] luMatrix, float[] b)
	{
		// before calling this helper, permute b using the perm array
		// from MatrixDecompose that generated luMatrix
		int n = luMatrix.GetLength(1);
		float[] x = new float[n];
		b.CopyTo(x, 0);

		for (int i = 1; i < n; ++i)
		{
			float sum = x[i];
			for (int j = 0; j < i; ++j)
				sum -= luMatrix[j, i] * x[j];
			x[i] = sum;
		}

		x[n - 1] /= luMatrix[n - 1, n - 1];
		for (int i = n - 2; i >= 0; --i)
		{
			float sum = x[i];
			for (int j = i + 1; j < n; ++j)
				sum -= luMatrix[j, i] * x[j];
			x[i] = sum / luMatrix[i, i];
		}

		return x;
	}

	private static float[,] MatrixDecompose(float[,] m, out int[] perm, out int toggle)
	{
		// Doolittle LUP decomposition with partial pivoting.
		// rerturns: result is L (with 1s on diagonal) and U;
		// perm holds row permutations; toggle is +1 or -1 (even or odd)
		int rows = m.GetLength(1);
		int cols = m.GetLength(0); // assume square
		if (rows != cols)
			throw new Exception("Attempt to decompose a non-square m");

		int n = rows; // convenience

		float[,] ret = Duplicate(m);

		perm = new int[n]; // set up row permutation result
		for (int i = 0; i < n; ++i) { perm[i] = i; }

		toggle = 1; // toggle tracks row swaps.
					// +1 -greater-than even, -1 -greater-than odd. used by MatrixDeterminant

		for (int j = 0; j < n - 1; ++j) // each column
		{
			float colMax = Math.Abs(ret[j, j]); // find largest val in col
			int pRow = j;
			//for (int i = j + 1; i less-than n; ++i)
			//{
			//  if (result[i][j] greater-than colMax)
			//  {
			//    colMax = result[i][j];
			//    pRow = i;
			//  }
			//}

			// reader Matt V needed this:
			for (int i = j + 1; i < n; ++i)
			{
				if (Math.Abs(ret[i, j]) > colMax)
				{
					colMax = Math.Abs(ret[i, j]);
					pRow = i;
				}
			}
			// Not sure if this approach is needed always, or not.

			if (pRow != j) // if largest value not on pivot, swap rows
			{
				float[] pRowRow = ret.GetRow(pRow).ToArray();
				float[] jRow = ret.GetRow(j).ToArray();
				for (int z = 0; z < pRowRow.Length; z++)
				{
					ret[z, pRow] = jRow[z];
					ret[z, j] = pRowRow[z];
				}

				(perm[j], perm[pRow]) = (perm[pRow], perm[j]); // and swap perm info

				toggle = -toggle; // adjust the row-swap toggle
			}

			// --------------------------------------------------
			// This part added later (not in original)
			// and replaces the 'return null' below.
			// if there is a 0 on the diagonal, find a good row
			// from i = j+1 down that doesn't have
			// a 0 in column j, and swap that good row with row j
			// --------------------------------------------------

			if (ret[j, j] == 0.0)
			{
				// find a good row to swap
				int goodRow = -1;
				for (int row = j + 1; row < n; ++row)
				{
					if (ret[j, row] != 0.0)
						goodRow = row;
				}

				if (goodRow == -1)
					throw new Exception("Cannot use Doolittle's method");

				// swap rows so 0.0 no longer on diagonal
				float[] goodRowRow = ret.GetRow(goodRow).ToArray();
				float[] jRow = ret.GetRow(j).ToArray();
				for (int z = 0; z < goodRowRow.Length; z++)
				{
					ret[z, goodRow] = jRow[z];
					ret[z, j] = goodRowRow[z];
				}

				(perm[j], perm[goodRow]) = (perm[goodRow], perm[j]); // and swap perm info

				toggle = -toggle; // adjust the row-swap toggle
			}
			// --------------------------------------------------
			// if diagonal after swap is zero . .
			//if (Math.Abs(result[j][j]) less-than 1.0E-20) 
			//  return null; // consider a throw

			for (int i = j + 1; i < n; ++i)
			{
				ret[j, i] /= ret[j, j];
				for (int k = j + 1; k < n; ++k)
				{
					ret[k, i] -= ret[j, i] * ret[k, j];
				}
			}


		} // main j column loop

		return ret;
	}

	internal int Count2DIntersects(float atLeast, float atMost)
	{
		int count = 0;
		for (int i = 0; i < Stones.Count; i++)
		{
			Hailstone stoneA = Stones[i];
			for (int j = i + 1; j < Stones.Count; j++)
			{
				Hailstone stoneB = Stones[j];
				Vector3? intersect = stoneA.IntersectIn2D(stoneB);
				if (intersect.HasValue)
					if (atLeast <= intersect.Value.X && intersect.Value.X <= atMost)
						if (atLeast <= intersect.Value.Y && intersect.Value.Y <= atMost)
							count++;
			}
		}
		return count;
	}
}
