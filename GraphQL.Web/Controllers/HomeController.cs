using GraphQL.Web.Clients;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ProductHttpClient _httpClient;
        private readonly ProductGraphClient _productGraphClient;

        public HomeController(ProductHttpClient httpClient, ProductGraphClient productGraphClient)
        {
            _httpClient = httpClient;
            _productGraphClient=productGraphClient;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> Index()
        {
            var responseModel = await _httpClient.GetProducts();
            responseModel.ThrowErrors();
            return Ok(responseModel.Data.Products);
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> ProductDetail(int productId)
        {
            var product = await _productGraphClient.GetProduct(productId);
            return Ok(product);
        }

        //public IActionResult AddReview(int productId)
        //{
        //    return Ok(new ProductReviewModel { ProductId = productId });
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddReview(ProductReviewInputModel reviewModel)
        //{
        //    await _productGraphClient.AddReview(reviewModel);
        //    return RedirectToAction("ProductDetail", new { productId = reviewModel.ProductId });
        //}
    }
}