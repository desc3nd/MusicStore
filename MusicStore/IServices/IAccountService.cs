using MusicStore.Models;


namespace MusicStore.IServices
{
    public interface IAccountService
    {
        public void AddAccuount(Account user);

        public bool IsAccountInDB();

        public bool LogIn(Account account);
    }
}
