using DocumentFormat.OpenXml.Presentation;
using System.Diagnostics.Metrics;

namespace CurdApplication.Models.Cascade
{
    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; } = default;
        // Ye kya karega state table m StateId k naam se ek cloumn add kardega no ki releation ho foregin key country aur state table ka
        public State State { get; set; }
    }
}
