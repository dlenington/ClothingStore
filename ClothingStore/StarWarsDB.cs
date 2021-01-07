using System.Collections.Generic;


namespace ClothingStore
{
    public static class StarWarsDB
    {
        public static IEnumerable<Jedi> GetJedis()
        {
            return new List<Jedi>() {
            new Jedi() { Name = "Luke", Side="Light", Id = 1},
            new Jedi() { Name = "Yoda", Side = "Light", Id = 2},
            new Jedi() { Name = "Darth Vader", Side = "Dark", Id = 3},
                };
        }

    }
}
