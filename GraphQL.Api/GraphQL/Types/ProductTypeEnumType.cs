using GraphQL.Api.Data.Entities;
using GraphQL.Types;

namespace GraphQL.Api.GraphQL.Types
{
    public class ProductTypeEnumType : EnumerationGraphType<ProductTypes>
    {
        public ProductTypeEnumType()
        {
            Name= "Type";
            Description = "The type of product";
        }
    }
}
