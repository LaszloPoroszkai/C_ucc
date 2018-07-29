namespace Villanyposta
{
    internal class User
    {
        string userName;
        string password;

        public User(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public string UserName { get => userName; }
        public string Password { get => password; }
    }
}