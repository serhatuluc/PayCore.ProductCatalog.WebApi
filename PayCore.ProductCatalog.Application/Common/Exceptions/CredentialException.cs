using System;


namespace PayCore.ProductCatalog.Application
{
    public class CredentialException : ApplicationException
    {
        public CredentialException (string message) : base(message)
        {

        }
    }
}
