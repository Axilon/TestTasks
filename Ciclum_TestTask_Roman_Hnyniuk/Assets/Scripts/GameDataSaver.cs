namespace DataSaver
{
    public abstract class GameDataSaver : IPrimitiveSavebl, IPrimitiveLoadbl
    {
        public abstract void SaveString(string key, string value);
        public abstract void SaveBool(string key, bool value);
        public abstract void SaveInt(string key, int value);
        public abstract void SaveFloat(string key, float value);

        public abstract string LoadString(string key);
        public abstract bool LoadBool(string key);
        public abstract int LoadInt(string key);
        public abstract float LoadFloat(string key);

        public virtual void SaveData() { }
    }
}
