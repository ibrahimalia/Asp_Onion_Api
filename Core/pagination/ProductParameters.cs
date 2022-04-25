namespace Core.pagination
{
    public class ProductParameters : RequestParameters
    {
        public int maxPrice { get; set; }
        public int minPrice { get; set; }
        public bool validaion => maxPrice < 9000 && minPrice > 5000;
        public string search {get; set;}
        public string orderBy {get; set;}
        

    }
}