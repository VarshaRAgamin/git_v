using System.ComponentModel.DataAnnotations;

namespace Vroom_Project.Models
{
    public class Make
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        
        public virtual IList<Model> Models { get; set; }
    }
}
