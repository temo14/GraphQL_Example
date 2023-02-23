﻿namespace GraphQL.Web.Models
{
    public class Response<T>
    {
        public T Data { get; set; }
        public List<ErrorModel> Errors { get; set; }

        public void ThrowErrors()
        {
            if (Errors != null && Errors.Any())
                throw new ApplicationException ($"Message: {Errors[0].Message} Code: {Errors[0].Code}");
        }
    }

    public class ProductsContainer
    {
        public List<ProductModel> Products { get; set; }
    }
}
