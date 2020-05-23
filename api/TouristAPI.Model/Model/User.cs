using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TouristAPI.Model
{
    public class User
    {
      [Key]
      public string Id { get; set; }

      [Required]
      public string Name { get; set; }
      
      public ICollection<Location> Locations { get; set; }
    }
}