namespace MongoDbExample.Dtos
{
    public class UserUpdateDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<BookDto> Books { get; set; }
    }
}
