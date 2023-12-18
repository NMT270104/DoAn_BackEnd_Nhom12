namespace WebAPI.DTO { 
// CategoryDTO.cs
    public class BookDTO
    {
        public string NameBook { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }


}