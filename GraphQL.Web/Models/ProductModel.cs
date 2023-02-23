﻿using Newtonsoft.Json;

namespace GraphQL.Web.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductTypeEnum Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset IntroducedAt { get; set; }
        public string PhotoFileName { get; set; }
        //public List<ProductReviewModel> Reviews { get; set; }
    }
    public enum ProductTypeEnum
    {
        Boots,
        ClimbingGear,
        Kayaks
    }
}
