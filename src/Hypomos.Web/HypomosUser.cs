namespace Hypomos.Web
{
    public class HypomosUser
    {
        public HypomosUser()
        {
            this.IsLoggedIn = false;
        }

        public HypomosUser(string username, string email, string givenName, string surname)
        {
            this.IsLoggedIn = true;

            this.Email = email;
            this.Surname = surname;
            this.GivenName = givenName;
            this.Username = username;
        }

        public bool IsLoggedIn { get; }

        public string Email { get; }
        public string Surname { get; }
        public string GivenName { get; }
        public string Username { get; }
    }
}