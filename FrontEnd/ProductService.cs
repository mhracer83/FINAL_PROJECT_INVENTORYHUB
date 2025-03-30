using System.Net.Http.Json;

public class ProductService
{
    private readonly HttpClient _httpClient;
    private Product[]? _cachedProducts;
    private DateTime _lastFetchedTime;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Product[]> GetProductsAsync()
    {
        if (_cachedProducts != null && DateTime.UtcNow - _lastFetchedTime < _cacheDuration)
        {
            return _cachedProducts;
        }

        try
        {
            var products = await _httpClient.GetFromJsonAsync<Product[]>("http://localhost:5160/api/productlist");
            if (products != null)
            {
                _cachedProducts = products;
                _lastFetchedTime = DateTime.UtcNow;
            }
            return _cachedProducts ?? Array.Empty<Product>();
        }
        catch
        {
            return Array.Empty<Product>();
        }
    }
}
