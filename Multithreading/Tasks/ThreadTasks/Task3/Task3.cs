using System;
using System.Threading.Tasks;

namespace ThreadTasks.Task3
{
    public static class Task3
    {
        public static void RunMatrixMultiplication()
        {
            int[][] firstMatrix = CreateMatrix();
            int[][] secondMatrix = CreateMatrix(firstMatrix[0].Length, firstMatrix.Length);

            Console.WriteLine("First Matrix");
            PrintMatrix(firstMatrix);
            Console.WriteLine("Second Matrix");
            PrintMatrix(secondMatrix);

            Console.WriteLine("Result Matrix");
            MultiplyMatrixes(firstMatrix, secondMatrix);
        }

        public static void MultiplyMatrixes(int[][] matrix1, int[][] matrix2)
        {
            int[][] result;

            if (matrix1 == null)
            {
                throw new ArgumentNullException(nameof(matrix1));
            } else if (matrix1.Length == 0)
            {
                throw new ArgumentException($"{nameof(matrix1)} is empty");
            }

            if (matrix2 == null)
            {
                throw new ArgumentNullException(nameof(matrix2));
            } else if (matrix2.Length == 0)
            {
                throw new ArgumentException($"{nameof(matrix2)} is empty");
            }

            if (!CheckArrayOnMatrix(matrix1))
            {
                throw new ArgumentException($"{nameof(matrix1)} is not a matrix");
            }

            if (!CheckArrayOnMatrix(matrix2))
            {
                throw new ArgumentException($"{nameof(matrix2)} is not a matrix");
            }

            int height = matrix1.Length;
            int width = matrix2[0].Length;

            if (height != width)
            {
                throw new ArgumentException($"{nameof(matrix1)} and {nameof(matrix2)} matrixes can't be multiplay.");
            }

            result = new int[height][];

            Parallel.ForEach(matrix1, (vector, state, index) =>
            {
                int[] resultVector = new int[width];

                for (int k = 0; k < width; k++)
                {
                    int temp = 0;
                    for (int j = 0; j < vector.Length; j++)
                    {
                        temp += vector[j] * matrix2[j][k];
                    }
                    resultVector[k] = temp;
                }

                result[index] = resultVector;
            });

            PrintMatrix(result);
        }

        private static int[][] CreateMatrix(int height = -1, int width = -1)
        {
            Random random = new Random();

            if (height < 0)
            {
                height = random.Next(1, 10);
            }

            if (width < 0)
            {
                width = random.Next(1, 10);
            }

            int[][] matrix = new int[height][];

            for (int i = 0; i < height; i++)
            {
                matrix[i] = new int[width];

                for (int j = 0; j < width; j++)
                {
                    matrix[i][j] = random.Next(0, 100);
                }
            }

            return matrix;
        }

        private static bool CheckArrayOnMatrix(int[][] matrix)
        {
            bool result = true;

            if (matrix.Length > 1)
            {
                for (int i = 1; i < matrix.Length; i++)
                {
                    if (matrix[i - 1].Length != matrix[i].Length)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        private static void PrintMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write($" {matrix[i][j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
