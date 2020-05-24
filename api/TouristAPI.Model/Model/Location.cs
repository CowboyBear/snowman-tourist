using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace TouristAPI.Model
{
  [Table("location")]
  public class Location
  {
    [Key]
    public int Id { get; set; }    

    [Required]
    public string UserId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public double Lat { get; set; }

    [Required]
    public double Lng { get; set; }

    [NotMapped]    
    public IFormFile Picture { get; set; }

    public string PicturePath { get; set; }    

    [ForeignKey("UserId")]
    public User User { get; set; }
  }
}
