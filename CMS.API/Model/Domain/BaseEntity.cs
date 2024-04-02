using System.ComponentModel.DataAnnotations;

namespace CMS.API.Model.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
