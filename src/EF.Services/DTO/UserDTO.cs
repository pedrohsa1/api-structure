namespace EF.Services.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserDTO()
        { }

        public UserDTO(long id, string uf, string password)
        {
            Id = id;
            Username = uf;
            Password = password;
        }
    }
}