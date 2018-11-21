namespace HPMS.Util
{
    public enum IndexType
    {
        Row,
        Column
    }


    class ArrayEnhance
    {
        /// <summary>
        /// 索引二维数组中的一行或一列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceArray"></param>
        /// <param name="index"></param>
        /// <param name="indexType"></param>
        /// <returns></returns>
        public static T[] IndexArray<T>(T[,] sourceArray, int index, IndexType indexType)
        {
            int rowCount = sourceArray.GetLength(0);
            int colCount = sourceArray.GetLength(1);
            //int size = index.Length;
            if (indexType==IndexType.Row)
            {
                T[] ret = new T[colCount];

                for (int i = 0; i < colCount; i++)
                {
                    ret[i] = sourceArray[index, i];
                }

                return ret;
            }
            else
            {
                T[] ret = new T[rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    ret[i] = sourceArray[i, index];

                }
                return ret;
            }
        }

        /// <summary>
        /// 返回数组中指定行或列构成的新的二维数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceArray"></param>
        /// <param name="index"></param>
        /// <param name="indexType"></param>
        /// <returns></returns>
        public static T[,] IndexArray<T>(T[,] sourceArray, int[] index, IndexType indexType)
        {

            int rowCount = sourceArray.GetLength(0);
            int colCount = sourceArray.GetLength(1);
            int size = index.Length;
            if (indexType == IndexType.Row)
            {
                T[,] ret = new T[size, colCount];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        ret[i, j] = sourceArray[index[i], j];
                    }
                }
                return ret;
            }
            else
            {
                T[,] ret = new T[rowCount, size];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        ret[i, j] = sourceArray[i, index[j]];
                    }
                }
                return ret;
            }

        }
    }
}
