using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace oktmo.Models
{
	public class OktmoEntry
	{
		[Key]
		public int Id { get; set; }

		[NotMapped]
		[StringLength(2)]
		[RegularExpression(@"^\d{2}$", ErrorMessage = "Ter must contain exactly 2 numeric characters.")]
		public string Ter => Octmo[..2];

        [NotMapped]
		public string Kod1 => Octmo[2..5];

		[NotMapped]
		public string Kod2 => Octmo[5..8];

        [NotMapped]
		public string Kod3 => Octmo[8..];

		[Required]
        [StringLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "octmo must contain exactly 11 numeric characters.")]
        public string Octmo { get; set; }

		[Required]
		[StringLength(1)]
		[RegularExpression(@"^\d{1}$", ErrorMessage = "Razdel must contain exactly 1 numeric character.")]
		public string Razdel { get; set; }

		[StringLength(500)]
		public string Name { get; set; }

		[Required]
		public bool IsActual { get; set; } = true;

	}
}
