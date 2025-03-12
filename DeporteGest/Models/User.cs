using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DeporteGest.Models;
public class User
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public required string Username { get; set; }

    [Required]
    [StringLength(255)]
    public required string Email { get; set; }

    [Required]
    public required string PasswordHash { get; set; }
}
