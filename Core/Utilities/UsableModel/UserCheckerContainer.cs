namespace SBA.MvcUI.Models.SettingsModels
{
    public class UserCheckerContainer
    {
        public UserCheckerContainer()
        {
            if (CheckUser == null)
            {
                CheckUser = new UserCheck();
            }
        }
        public string Serials { get; set; }
        public UserCheck CheckUser { get; set; }
    }

    public class UserCheck
    {
        private const long _onurID = 1093967965;
        private const long _myID = 5532339449;
        private const long _eldarID = 5065439386;

        public bool IsOnurChecked { get; set; }
        public long OnurID => _onurID;
        public bool IsMeChecked { get; set; }
        public long MyID => _myID;
        public bool IsEldarChecked { get; set; }
        public long EldarID => _eldarID;
    }
}
