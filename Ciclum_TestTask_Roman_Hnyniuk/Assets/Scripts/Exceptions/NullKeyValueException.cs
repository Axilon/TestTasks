
using System;
namespace DataSaver
{
    public class NullKeyValueException : ArgumentException
    {
        public NullKeyValueException(string value)
            : base(String.Format("There is no value in Data for \"{0}\" key", value))
        {
        }
    }
}
