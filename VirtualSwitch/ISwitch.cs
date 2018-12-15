namespace VirtualSwitch
{
    public interface ISwitch
    {
        bool CloseAll(ref string errMsg);
        bool Open(int switchIndex, ref string errMsg);
        bool Open(byte[] switchNum, ref string errMsg);
        bool OpenS(int switchIndex, ref string errMsg);
        bool OpenS(byte[] switchNum, ref string errMsg);
    }
    enum IndexType
    {
        Row,
        Column
    }
}
