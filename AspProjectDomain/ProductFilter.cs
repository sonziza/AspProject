namespace AspProjectDomain
{
    public class ProductFilter
    {
        public int? SectionId { get; init; }

        public int? BrandId { get; init; }

        //массив идентификаторов
        public int[] Ids { get; set; }
    }
}
