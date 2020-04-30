using System;

namespace DotVVM.AMP.Validator
{
    public class AmpException : Exception
    {
        public AmpException(string message, Exception innerException=null) : base(message,innerException)
        {
        }
    }
}