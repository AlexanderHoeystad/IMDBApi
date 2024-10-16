using System.ComponentModel.DataAnnotations;

namespace IMDBApi
{
    public class Crew
    {
        [Key]
        public string Tconst { get; set; }
        public string Directors { get; set; }
        public string Writers { get; set; }

    }
}
