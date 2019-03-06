namespace DataSaver
{
    public interface IPrimitiveSavebl
    {
        void SaveString(string key, string value);
        void SaveBool(string key, bool value);
        void SaveInt(string key, int value);
        void SaveFloat(string key, float value);
    }
}
