// Authors.cs
using System.ComponentModel.DataAnnotations;

namespace ApiProject.Models
{
    public class Author
    {

        public int Id { get; set; }
        [StringLength(20)] public string? Name { get; set; }
        public string? Country { get; set; }

    }
}