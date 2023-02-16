namespace ExtensionMethods
{
    public static class HeroExtensions  //static class/method is not instantiated
    {
     
        //assuming we don't own the code to Hero, this is a way for us to extend the hero functionality
        //`hero.Tier()` intead of `GetHeroTier(hero)`
        public static string Tier(this Hero hero)  //pass instance of hero to this method
        {
            if (hero.Name == null)
            {
                throw new NullReferenceException("hero cannot be null");
            }
            return hero.Name.Substring(0,1);
        }
    }
}