namespace CurdApplication.Models.Cascade
{
    public class State
    {
        public int Id { get; set; }
        public string StateName { get; set; } = default;

        // Ye kya karega state table m CountryId k naam se ek cloumn add kardega no ki releation ho foregin key country aur state table ka
        public Country Country { get; set; }
    }
}
