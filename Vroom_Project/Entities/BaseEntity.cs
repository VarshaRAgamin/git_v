using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Vroom_Project.Entities
{
    public class BaseEntity: IBaseEntity
    {
        public int Id { get; set; }
        [ForeignKey("CreatedUser")]
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("ModifiedUser")]
        public int? ModifiedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
