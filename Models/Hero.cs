public class Hero
{
    public string? Key { get; set; }
    public string? Name { get; set; }
    public string? Portrait { get; set; }
    public string? Role { get; set; }  

    public static async Task<IList<Hero>?> GetHeroesAsync()  //Should this be in the hero model or a service?
    {
        try
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://overfast-api.tekrop.fr/heroes"); //("https://overwatch-hero-api.herokuapp.com/api/v1/heroes");
            //TODO: google herokuapp service, is there an azure heroku or gov equivalent
            //cloud service, similar to Azure

            if (response.IsSuccessStatusCode)
            {
                //TODO: catch exceptions coming from ReadFromJsonAsync
                var heroList = response.Content.ReadFromJsonAsync<IList<Hero>>();  
                return heroList.Result;  
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