namespace BookLibrary.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public Author Auth { get; set; }
        
        public override string ToString()
        {
            return $"{Title} ({PublicationYear})";
        }
    }
}