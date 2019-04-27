namespace RomMapConvert.Interface
{
    internal interface IConvert
    {
        string GetVersion();
        byte[][] GetRomMapCross(string pn, string sn);
        byte[,] GetRomMapArray(string pn, string sn);
    }
}
