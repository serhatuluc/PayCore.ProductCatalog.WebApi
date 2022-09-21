using System;

namespace PayCore.ProductCatalog.Application
{
    public class BadRequestException:ApplicationException
    {
        public BadRequestException(string message) : base(message)
        {

        }
    }
}
