using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VirtualSwitch;

namespace SwitchBoxDebug
{
    public enum BoxType
    {
        Switch40,
        Switch48,
        Switch80
    }


    public enum MapType
    {
        UpRowPrior,
        Half

    }
    public enum IndexType
    {
        Row,
        Column
    }

    public enum Ext
    {
        Next,
        Fext
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
            if (indexType == IndexType.Row)
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

    public class Util
    {

        private static string[] _next2 =
        {
            "TX1-RX1"
        };

        private static string[] _next8 =
        {
            "TX1-RX1","TX2-RX1","TX3-RX1","TX4-RX1",
            "TX1-RX2","TX2-RX2","TX3-RX2","TX4-RX2",
            "TX1-RX3","TX2-RX3","TX3-RX3","TX4-RX3",
            "TX1-RX4","TX2-RX4","TX3-RX4","TX4-RX4"
        };

        private static string[] _fext8 =
        {
            "TX2-RX1","TX3-RX1","TX4-RX1",
            "TX1-RX2","TX3-RX2","TX4-RX2",
            "TX1-RX3","TX2-RX3","TX4-RX3",
            "TX1-RX4","TX2-RX4","TX3-RX4"
        };

        private static string[] _next16 =
        {
            "TX1-RX1","TX2-RX1","TX3-RX1","TX4-RX1",
            "TX1-RX2","TX2-RX2","TX3-RX2","TX4-RX2",
            "TX1-RX3","TX2-RX3","TX3-RX3","TX4-RX3",
            "TX1-RX4","TX2-RX4","TX3-RX4","TX4-RX4",
            "TX5-RX5","TX6-RX5","TX7-RX5","TX8-RX5",
            "TX5-RX6","TX6-RX6","TX7-RX6","TX8-RX6",
            "TX5-RX7","TX6-RX7","TX7-RX7","TX8-RX7",
            "TX5-RX8","TX6-RX8","TX7-RX8","TX8-RX8"
        };

        private static string[] _fext16 =
        {
            "TX2-RX1","TX3-RX1","TX4-RX1",
            "TX1-RX2","TX3-RX2","TX4-RX2",
            "TX1-RX3","TX2-RX3","TX4-RX3",
            "TX1-RX4","TX2-RX4","TX3-RX4",
            "TX6-RX5","TX7-RX5","TX8-RX5",
            "TX5-RX6","TX7-RX6","TX8-RX6",
            "TX5-RX7","TX6-RX7","TX8-RX7",
            "TX5-RX8","TX6-RX8","TX7-RX8"
        };
        public static string[,] GetDirectConfig(int pair, MapType mapType, int boxBlock)
        {
            string[,] ret = new string[pair * 2, 4];

            for (int i = 0; i < pair / 2; i++)
            {
                ret[2 * i, 0] = "TX" + (i + 1) + "+";
                ret[2 * i, 1] = "TX" + (i + 1) + "-";
                ret[2 * i, 2] = "RX" + (i + 1) + "+";
                ret[2 * i, 3] = "RX" + (i + 1) + "-";

                ret[2 * i + 1, 0] = "A1" + i;
                ret[2 * i + 1, 1] = "A3" + i;
                ret[2 * i + 1, 2] = "A5" + i;
                ret[2 * i + 1, 3] = "A7" + i;
            }

            for (int i = 0; i < pair / 2; i++)
            {
                ret[2 * i + pair, 0] = "RX" + (i + 1) + "+";
                ret[2 * i + pair, 1] = "RX" + (i + 1) + "-";
                ret[2 * i + pair, 2] = "TX" + (i + 1) + "+";
                ret[2 * i + pair, 3] = "TX" + (i + 1) + "-";

                if (mapType == MapType.UpRowPrior)
                {
                    if (boxBlock - pair / 2 > i)
                    {
                        ret[2 * i + pair + 1, 0] = "A5" + (i + pair / 2);
                        ret[2 * i + pair + 1, 1] = "A7" + (i + pair / 2);
                        ret[2 * i + pair + 1, 2] = "A1" + (i + pair / 2);
                        ret[2 * i + pair + 1, 3] = "A3" + (i + pair / 2);
                    }
                    else
                    {
                        ret[2 * i + pair + 1, 0] = "B6" + (i - (boxBlock - pair / 2));
                        ret[2 * i + pair + 1, 1] = "B8" + (i - (boxBlock - pair / 2));
                        ret[2 * i + pair + 1, 2] = "B2" + (i - (boxBlock - pair / 2));
                        ret[2 * i + pair + 1, 3] = "B4" + (i - (boxBlock - pair / 2));
                    }

                }
                else
                {
                    ret[2 * i + pair + 1, 0] = "B6" + i;
                    ret[2 * i + pair + 1, 1] = "B8" + i;
                    ret[2 * i + pair + 1, 2] = "B2" + i;
                    ret[2 * i + pair + 1, 3] = "B4" + i;
                }

            }

            return ret;
        }

       


        public static string[][] GetExt(string[,] current,string pair,Ext ext)
        {
            if (pair == "2")
            {
                if (ext == Ext.Fext)
                {
                    throw new Exception("2 pair has no fext");
                }
                else
                {
                    string[] pairArray = GetPairArray(current, ext);
                    string[] indexArray = GetIndexArray(current, ext);

                    int row = current.GetLength(0);
                    int col = current.GetLength(1);
                    string[,] currentReverse=new string[row,col];
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            currentReverse[i, j] = current[i, (j + 2)%4];
                        }
                    }

                    string[] pairArrayReverse = GetPairArray(currentReverse, ext);
                    string[] indexArrayReverse = GetIndexArray(currentReverse, ext);
                    string[][] p1 = GetExt(pairArray, indexArray, GetPairConst(pair, ext));
                    string[][] p2 = GetExt(pairArrayReverse, indexArrayReverse, GetPairConst(pair, ext));

                    List<string[]>tempList=new List<string[]>();
                    tempList.AddRange(p1.ToList());
                    tempList.AddRange(p2.ToList());
                    return tempList.ToArray();
                }
            }
            else
            {
                string[] pairArray = GetPairArray(current, ext);
                string[] indexArray = GetIndexArray(current, ext);

                return GetExt(pairArray, indexArray, GetPairConst(pair, ext));
            }
        }

        private static string[] GetPairArray(string[,] current,Ext ext)
        {
            List<string>ret=new List<string>();
            int length = current.GetLength(0);
            if (ext == Ext.Next)
            {
                for (int i = 0; i < length/2; i++)
                {
                    ret.Add(current[i*2,0]);
                    ret.Add(current[i * 2, 1]);
                }
            }
            else
            {
                for (int i = 0; i < length/4; i++)
                {
                    ret.Add(current[i * 2, 0]);
                    ret.Add(current[i * 2, 1]);
                    ret.Add(current[i * 2, 2]);
                    ret.Add(current[i * 2, 3]);
                }
            }

            return ret.ToArray();
        }

        private static string[] GetIndexArray(string[,] current, Ext ext)
        {
            List<string> ret = new List<string>();
            int length = current.GetLength(0);
            if (ext == Ext.Next)
            {
                for (int i = 0; i < length / 2; i++)
                {
                    ret.Add(current[i * 2+1, 0]);
                    ret.Add(current[i * 2+1, 1]);
                }
            }
            else
            {
                for (int i = 0; i < length / 4; i++)
                {
                    ret.Add(current[i * 2+1, 0]);
                    ret.Add(current[i * 2+1, 1]);
                    ret.Add(current[i * 2+1, 2]);
                    ret.Add(current[i * 2+1, 3]);
                }
            }

            return ret.ToArray();
        }

        private static string[] GetPairConst(string pair,Ext ext)
        {
            if (pair == "8")
            {
                if (ext == Ext.Next)
                    return _next8;
                return _fext8;

            }
            else if(pair=="16")
            {
                if (ext == Ext.Next)
                    return _next16;
                return _fext16;
            }
            else
            {
                return _next2;
            }
        }

        private static string[][] GetExt(string[] directPair, string[] directSwitch,string[]ext)
        {
            int length = ext.Length;
            string[][] ret = new string[length * 2][];
            for (int i = 0; i < length; i++)
            {
                ret[2*i+1] = GetSwitchIndex(directPair, directSwitch, ext[i]);
                ret[2*i] = GetPairIndex(ext[i]);
            }

            return ret;
        }


        
        private static string[] GetSwitchIndex(string[] directPair,string[]directSwitch,string pairName)
        {
            string[]ret=new string[4];
            string[] txrx = pairName.Split('-');

            int indexTxP = SwitchUtil.FindIndex(directPair, txrx[0] + "+");
            int indexTxN = SwitchUtil.FindIndex(directPair,txrx[0] + "-");
            int indexRxP = SwitchUtil.FindIndex(directPair,txrx[1] + "+");
            int indexRxN = SwitchUtil.FindIndex(directPair,txrx[1] + "-");
            ret[0] = directSwitch[indexTxP];
            ret[1] = directSwitch[indexTxN];
            ret[2] = directSwitch[indexRxP];
            ret[3] = directSwitch[indexRxN];
            return ret;
        }

        private static string[] GetPairIndex(string pairName)
        {
            string[] ret = new string[4];
            string[] txrx = pairName.Split('-');

            ret[0] = txrx[0] + "+";
            ret[1] = txrx[0] + "-";
            ret[2] = txrx[1] + "+";
            ret[3] = txrx[1] + "-";
            return ret;
        }

        public static string[,] ToStringArray(DataGridView dataGridView, bool includeColumnText)
        {
            #region 实现...

            string[,] arrReturn = null;
            int rowsCount = dataGridView.Rows.Count;
            int colsCount = dataGridView.Columns.Count;
            if (rowsCount > 0)
            {
                //最后一行是供输入的行时，不用读数据。
                if (dataGridView.Rows[rowsCount - 1].IsNewRow)
                {
                    rowsCount--;
                }
            }

            int i = 0;
                //包括列标题
            if (includeColumnText)
            {
                rowsCount++;
                arrReturn = new string[rowsCount, colsCount];
                for (i = 0; i < colsCount; i++)
                {
                    arrReturn[0, i] = dataGridView.Columns[i].HeaderText;
                }

                i = 1;
            }
            else
            {
                arrReturn = new string[rowsCount, colsCount];
            }

            //读取单元格数据
            int rowIndex = 0;
            for (; i < rowsCount; i++, rowIndex++)
            {
                for (int j = 0; j < colsCount; j++)
                {
                    arrReturn[i, j] = dataGridView.Rows[rowIndex].Cells[j].Value.ToString();
                }

             
            }
            return arrReturn;

            #endregion 实现
        }
    }
}