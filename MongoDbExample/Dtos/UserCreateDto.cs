using MongoDbExample.Models;

namespace MongoDbExample.Dtos
{
    public class UserCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<BookDto> Books { get; set; }
    }
}
