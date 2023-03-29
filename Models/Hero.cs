public class Hero
{
    public string? Key { get; set; }
    public string? Name { get; set; }
    public string? Portrait { get; set; }
    public string? Role { get; set; }  

    public static async Task<IList<Hero>?> GetHeroesAsync(ILogger _logger) 
    {
        throw new Exception("Received bad status code from Overwatch API: 500");
        
        HttpResponseMessage? response = null; 
        try
        {
            _logger.LogDebug("log before GetHeroes");
            HttpClient client = new HttpClient();
            response = await client.GetAsync("https://overfast-api.tekrop.fr/heroes"); 


            _logger.LogDebug("log after GetHeroes");
            if (!response.IsSuccessStatusCode)
            {
                //TODO: display error page, handle different status codes
                Exception ex = new Exception("Received bad status code from Overwatch API: " + response.StatusCode);
                throw ex;
            }
        }
        //TODO: handle client async exceptions
        catch(Exception ex) 
        {
            ex = new Exception("Error communicating with Overwatch API");
            throw ex;              
        }

        try
        {
            var heroList = response.Content.ReadFromJsonAsync<IList<Hero>>(); 
            return heroList.Result;   
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public static async Task<Hero?> GetHeroDetailAsync(string key) 
    {
        try
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://overfast-api.tekrop.fr/heroes/" + key); 
            
            if (response.IsSuccessStatusCode)
            {
                var heroDetail = response.Content.ReadFromJsonAsync<Hero>();  
                return heroDetail.Result;  
            }
            else
            {
                Exception ex = new Exception("received error from Overwatch API");
                throw ex;
            }
        }
        catch(HttpRequestException e) //TODO: display error page, handle different error codes
        {
            throw e;              
        }
        //finally{} runs regardless, might use this method do cleanup, close db, etc
    }

    public override string ToString()
    {
        return Name + ": " + Role;
    }
}