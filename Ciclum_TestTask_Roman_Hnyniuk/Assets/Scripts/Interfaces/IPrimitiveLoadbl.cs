namespace DataSaver
{
    public interface IPrimitiveLoadbl
    {
        string LoadString(string key);
        bool LoadBool(string key);
        int LoadInt(string key);
        float LoadFloat(string key);
    }
}
